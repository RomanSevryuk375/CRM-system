using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.TestHelpers;
using FluentAssertions;
using Xunit;

namespace CRM.Billing.UnitTests.Domain.Entities;

public class BillTests
{
    private static readonly DateOnly Today = new(2026, 9, 11);
    private static readonly DateTimeOffset TodayDateTime = new(2026, 9, 11, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var billId = new BillId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());

        // Act
        var result = Bill.Create(billId, orderId, BillStatus.Unpaid, 1000m, null, Today);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(billId);
        result.Value.OrderId.Should().Be(orderId);
        result.Value.Status.Should().Be(BillStatus.Unpaid);
        result.Value.Amount.Value.Should().Be(1000m);
        result.Value.ActualBillDate.Should().BeNull();
        result.Value.PaymentNotes.Should().BeEmpty();
        result.Value.TotalPaidAmount.Should().Be(0m);
    }

    [Fact]
    public void Create_WithPaidStatusAndActualDate_ReturnsSuccess()
    {
        // Arrange
        var billId = new BillId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());

        // Act
        var result = Bill.Create(billId, orderId, BillStatus.Paid, 500m, Today, Today);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Status.Should().Be(BillStatus.Paid);
        result.Value.ActualBillDate.Should().Be(Today);
    }

    [Fact]
    public void Create_WithNegativeAmount_ReturnsFailure()
    {
        // Arrange
        var billId = new BillId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());

        // Act
        var result = Bill.Create(billId, orderId, BillStatus.Unpaid, -100m, null, Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Money.Errors.NegativeValue);
    }

    [Fact]
    public void Create_WithPaidStatusButNoActualDate_ReturnsFailure()
    {
        // Arrange
        var billId = new BillId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());

        // Act
        var result = Bill.Create(billId, orderId, BillStatus.Paid, 500m, null, Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Bill.Errors.ActualDateRequiredForPaid);
    }

    [Fact]
    public void Create_WithFutureActualDate_ReturnsFailure()
    {
        // Arrange
        var billId = new BillId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());
        var futureDate = Today.AddDays(1);

        // Act
        var result = Bill.Create(billId, orderId, BillStatus.Paid, 500m, futureDate, Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Bill.Errors.FutureActualDate);
    }

    [Fact]
    public void CloseByUser_WhenUnpaid_ReturnsSuccessAndStatusPaid()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();

        // Act
        var result = bill.CloseByUser(Today);

        // Assert
        result.IsSuccess.Should().BeTrue();
        bill.Status.Should().Be(BillStatus.Paid);
        bill.ActualBillDate.Should().Be(Today);
    }

    [Fact]
    public void CloseByUser_WhenAlreadyPaid_ReturnsFailureWithConflict()
    {
        // Arrange
        var bill = BillMother.CreatePaid();

        // Act
        var result = bill.CloseByUser(Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Bill.Errors.BillAlreadyPaid);
    }

    [Fact]
    public void AddPaymentNote_WhenPartialPayment_RecalculatesStatusToPartiallyPaid()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();
        var paymentId = new PaymentNoteId(Guid.NewGuid());

        // Act
        var result = bill.AddPaymentNote(paymentId, 400m, PaymentMethod.Card, TodayDateTime, TodayDateTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        bill.PaymentNotes.Should().HaveCount(1);
        bill.TotalPaidAmount.Should().Be(400m);
        bill.Status.Should().Be(BillStatus.PartiallyPaid);
        bill.ActualBillDate.Should().BeNull();
    }

    [Fact]
    public void AddPaymentNote_WhenFullAmountPaid_RecalculatesStatusToPaid()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();
        var paymentId = new PaymentNoteId(Guid.NewGuid());

        // Act
        var result = bill.AddPaymentNote(paymentId, 1000m, PaymentMethod.Cash, TodayDateTime, TodayDateTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        bill.Status.Should().Be(BillStatus.Paid);
        bill.ActualBillDate.Should().Be(Today);
        bill.TotalPaidAmount.Should().Be(1000m);
    }

    [Fact]
    public void AddPaymentNote_WhenBillAlreadyPaid_ReturnsFailure()
    {
        // Arrange
        var bill = BillMother.CreatePaid();

        // Act
        var result = bill.AddPaymentNote(new PaymentNoteId(Guid.NewGuid()), 100m, PaymentMethod.Cash, TodayDateTime, TodayDateTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Bill.Errors.CannotAddPaymentToPaidBill);
    }

    [Fact]
    public void AddPaymentNote_WithFutureDate_ReturnsFailure()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();
        var futureDate = TodayDateTime.AddDays(1);

        // Act
        var result = bill.AddPaymentNote(new PaymentNoteId(Guid.NewGuid()), 500m, PaymentMethod.Cash, futureDate, TodayDateTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Bill.Errors.FuturePaymentDate);
    }

    [Fact]
    public void AddPaymentNote_ExceedsBalance_ReturnsFailure()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();

        // Act
        var result = bill.AddPaymentNote(new PaymentNoteId(Guid.NewGuid()), 1500m, PaymentMethod.Cash, TodayDateTime, TodayDateTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Bill.Errors.PaymentExceedsBalance);
    }

    [Fact]
    public void AddPaymentNote_WithNegativeAmount_ReturnsFailure()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();

        // Act
        var result = bill.AddPaymentNote(new PaymentNoteId(Guid.NewGuid()), -100m, PaymentMethod.Cash, TodayDateTime, TodayDateTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Money.Errors.NegativeValue);
    }

    [Fact]
    public void RemovePaymentNote_WhenExists_RemovesAndRecalculatesStatus()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();
        var paymentId = new PaymentNoteId(Guid.NewGuid());
        bill.AddPaymentNote(paymentId, 1000m, PaymentMethod.Cash, TodayDateTime, TodayDateTime);
        bill.Status.Should().Be(BillStatus.Paid);

        // Act
        var result = bill.RemovePaymentNote(paymentId, TodayDateTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        bill.PaymentNotes.Should().BeEmpty();
        bill.TotalPaidAmount.Should().Be(0m);
        bill.Status.Should().Be(BillStatus.Unpaid);
        bill.ActualBillDate.Should().BeNull();
    }

    [Fact]
    public void RemovePaymentNote_WhenNotFound_ReturnsFailure()
    {
        // Arrange
        var bill = BillMother.CreateUnpaid();
        var nonExistentId = new PaymentNoteId(Guid.NewGuid());

        // Act
        var result = bill.RemovePaymentNote(nonExistentId, TodayDateTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Bill.Errors.PaymentNoteNotFound);
    }
}
