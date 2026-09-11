using CRM.Ordering.Domain.Entities.Orders;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Ordering.UnitTests.Domain.Entities;

public class OrderPartTests
{
    private static readonly OrderId TestOrderId = new(Guid.NewGuid());
    private static readonly PartId TestPartId = new(Guid.NewGuid());

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var partId = new OrderPartId(Guid.NewGuid());
        var price = Money.Create(150m).Value;

        // Act
        var result = OrderPart.Create(
            partId,
            TestOrderId,
            TestPartId,
            4m,
            price,
            isProposed: false);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(partId);
        result.Value.OrderId.Should().Be(TestOrderId);
        result.Value.PartId.Should().Be(TestPartId);
        result.Value.Quantity.Should().Be(4m);
        result.Value.SoldPrice.Should().Be(price);
        result.Value.IsProposed.Should().BeFalse();
    }

    [Fact]
    public void Create_WithNegativeQuantity_ReturnsFailure()
    {
        // Act
        var result = OrderPart.Create(
            new OrderPartId(Guid.NewGuid()),
            TestOrderId,
            TestPartId,
            -1m,
            Money.Create(100m).Value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderPart.Errors.NegativeQuantity);
    }

    [Fact]
    public void SetQuantity_WithZero_ReturnsSuccess()
    {
        // Arrange
        var part = OrderPart.Create(
            new OrderPartId(Guid.NewGuid()),
            TestOrderId,
            TestPartId,
            5m,
            Money.Create(100m).Value).Value;

        // Act
        var result = part.SetQuantity(0m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        part.Quantity.Should().Be(0m);
    }

    [Fact]
    public void SetQuantity_WithNegative_ReturnsFailure()
    {
        // Arrange
        var part = OrderPart.Create(
            new OrderPartId(Guid.NewGuid()),
            TestOrderId,
            TestPartId,
            5m,
            Money.Create(100m).Value).Value;

        // Act
        var result = part.SetQuantity(-2m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(OrderPart.Errors.NegativeQuantity);
    }

    [Fact]
    public void SetSoldPrice_UpdatesPrice()
    {
        // Arrange
        var part = OrderPart.Create(
            new OrderPartId(Guid.NewGuid()),
            TestOrderId,
            TestPartId,
            5m,
            Money.Create(100m).Value).Value;

        var newPrice = Money.Create(200m).Value;

        // Act
        var result = part.SetSoldPrice(newPrice);

        // Assert
        result.IsSuccess.Should().BeTrue();
        part.SoldPrice.Should().Be(newPrice);
    }

    [Fact]
    public void MarkAsProposed_WhenNotProposed_SetsIsProposedTrue()
    {
        // Arrange
        var part = OrderPart.Create(
            new OrderPartId(Guid.NewGuid()),
            TestOrderId,
            TestPartId,
            1m,
            Money.Create(50m).Value,
            isProposed: false).Value;

        // Act & Assert
        part.MarkAsProposed().IsSuccess.Should().BeTrue();
        part.IsProposed.Should().BeTrue();
    }

    [Fact]
    public void Approve_WhenProposed_SetsIsProposedFalse()
    {
        // Arrange
        var part = OrderPart.Create(
            new OrderPartId(Guid.NewGuid()),
            TestOrderId,
            TestPartId,
            1m,
            Money.Create(50m).Value,
            isProposed: true).Value;

        // Act & Assert
        part.Approve().IsSuccess.Should().BeTrue();
        part.IsProposed.Should().BeFalse();
    }
}
