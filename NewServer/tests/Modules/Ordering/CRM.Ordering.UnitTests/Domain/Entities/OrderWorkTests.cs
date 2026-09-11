using CRM.TestHelpers;
using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Ordering.UnitTests.Domain.Entities;

public class OrderWorkTests
{
    private static readonly OrderId TestOrderId = new(Guid.NewGuid());
    private static readonly JobId TestJobId = new(Guid.NewGuid());
    private static readonly WorkerId TestWorkerId = new(Guid.NewGuid());

    [Fact]
    public void Create_WithFixedPrice_ReturnsSuccess_TotalCostEqualsFixedPrice()
    {
        // Arrange
        var workId = new OrderWorkId(Guid.NewGuid());
        var fixedPrice = Money.Create(250m).Value;

        // Act
        var result = OrderWork.Create(
            workId,
            TestOrderId,
            TestJobId,
            WorkStatus.Pending,
            hourlyRate: null,
            StandardHours.Create(3m).Value,
            fixedPrice,
            isProposed: false);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(workId);
        result.Value.FixedPrice.Should().Be(fixedPrice);
        result.Value.TotalCost.Should().Be(fixedPrice);
        result.Value.HourlyRate.Should().BeNull();
        result.Value.Status.Should().Be(WorkStatus.Pending);
    }

    [Fact]
    public void Create_WithHourlyRateAndHours_ComputesTotalCost()
    {
        // Arrange
        var workId = new OrderWorkId(Guid.NewGuid());
        var rate = Money.Create(80m).Value;
        var hours = StandardHours.Create(2.5m).Value;

        // Act
        var result = OrderWork.Create(
            workId,
            TestOrderId,
            TestJobId,
            WorkStatus.Pending,
            rate,
            hours,
            fixedPrice: null,
            isProposed: false);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.HourlyRate.Should().Be(rate);
        result.Value.EstimatedHours.Should().Be(hours);
        result.Value.TotalCost!.Value.Should().Be(200m); // 80 * 2.5
    }

    [Fact]
    public void Create_WithNoRateAndNoFixedPrice_ReturnsFailure()
    {
        // Act
        var result = OrderWork.Create(
            new OrderWorkId(Guid.NewGuid()),
            TestOrderId,
            TestJobId,
            WorkStatus.Pending,
            hourlyRate: null,
            StandardHours.Create(2m).Value,
            fixedPrice: null,
            isProposed: false);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderWork.Errors.MissingPriceOrRate);
    }

    [Fact]
    public void Create_WithCompletedStatus_ReturnsFailure()
    {
        // Act
        var result = OrderWork.Create(
            new OrderWorkId(Guid.NewGuid()),
            TestOrderId,
            TestJobId,
            WorkStatus.Completed,
            Money.Create(50m).Value,
            StandardHours.Create(1m).Value,
            null,
            isProposed: false);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderWork.Errors.CannotBeCompleted);
    }

    [Fact]
    public void BeginWork_SetsStatusInProgressAndAssignsWorker()
    {
        // Arrange
        var work = OrderWorkMother.CreatePending();

        // Act
        var result = work.BeginWork(TestWorkerId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        work.Status.Should().Be(WorkStatus.InProgress);
        work.WorkerId.Should().Be(TestWorkerId);
    }

    [Fact]
    public void CompleteWork_WithoutWorker_ReturnsFailure()
    {
        // Arrange
        var work = OrderWorkMother.CreatePending();

        // Act
        var result = work.CompleteWork(StandardHours.Create(2m).Value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(OrderWork.Errors.WorkerNotAssigned);
    }

    [Fact]
    public void CompleteWork_WhenValid_SetsStatusCompletedAndTimeSpent()
    {
        // Arrange
        var work = OrderWorkMother.CreatePending();

        work.BeginWork(TestWorkerId);
        var timeSpent = StandardHours.Create(2.5m).Value;

        // Act
        var result = work.CompleteWork(timeSpent);

        // Assert
        result.IsSuccess.Should().BeTrue();
        work.Status.Should().Be(WorkStatus.Completed);
        work.TimeSpent.Should().Be(timeSpent);
    }

    [Fact]
    public void CompleteWork_WhenAlreadyCompleted_ReturnsFailure()
    {
        // Arrange
        var work = OrderWorkMother.CreatePending();

        work.BeginWork(TestWorkerId);
        work.CompleteWork(StandardHours.Create(2m).Value);

        // Act
        var result = work.CompleteWork(StandardHours.Create(3m).Value);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(OrderWork.Errors.AlreadyCompleted);
    }

    [Fact]
    public void MarkAsProposed_WhenPending_SetsIsProposed()
    {
        // Arrange
        var work = OrderWork.Create(
            new OrderWorkId(Guid.NewGuid()),
            TestOrderId,
            TestJobId,
            WorkStatus.Pending,
            Money.Create(50m).Value,
            StandardHours.Create(2m).Value,
            null,
            isProposed: false).Value;

        // Act
        var result = work.MarkAsProposed();

        // Assert
        result.IsSuccess.Should().BeTrue();
        work.IsProposed.Should().BeTrue();
    }

    [Fact]
    public void Approve_WhenProposed_SetsIsProposedFalse()
    {
        // Arrange
        var work = OrderWork.Create(
            new OrderWorkId(Guid.NewGuid()),
            TestOrderId,
            TestJobId,
            WorkStatus.Pending,
            Money.Create(50m).Value,
            StandardHours.Create(2m).Value,
            null,
            isProposed: true).Value;

        // Act
        var result = work.Approve();

        // Assert
        result.IsSuccess.Should().BeTrue();
        work.IsProposed.Should().BeFalse();
    }
}

