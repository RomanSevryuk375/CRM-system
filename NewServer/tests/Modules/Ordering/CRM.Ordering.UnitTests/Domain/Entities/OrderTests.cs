using CRM.Ordering.Domain.Entities.Orders;
using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.TestHelpers;
using FluentAssertions;
using Xunit;

namespace CRM.Ordering.UnitTests.Domain.Entities;

public class OrderTests
{
    private static readonly CarId TestCarId = new(Guid.NewGuid());
    private static readonly WorkerId TestWorkerId = new(Guid.NewGuid());
    private static readonly DateOnly StartDate = new(2026, 9, 1);
    private static readonly DateOnly FinishDate = new(2026, 9, 15);

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var orderId = new OrderId(Guid.NewGuid());

        // Act
        var result = Order.Create(
            orderId,
            TestCarId,
            TestWorkerId,
            StartDate,
            FinishDate,
            OrderPriority.Medium);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(orderId);
        result.Value.Status.Should().Be(OrderStatus.Pending);
        result.Value.CarId.Should().Be(TestCarId);
        result.Value.WorkerId.Should().Be(TestWorkerId);
        result.Value.StartedAt.Should().Be(StartDate);
        result.Value.PlannedFinishDate.Should().Be(FinishDate);
        result.Value.Priority.Should().Be(OrderPriority.Medium);
        result.Value.Amount.Value.Should().Be(0m);
    }

    [Fact]
    public void Create_WhenPlannedDateBeforeStart_ReturnsFailure()
    {
        // Arrange
        var earlierFinish = StartDate.AddDays(-1);

        // Act
        var result = Order.Create(
            new OrderId(Guid.NewGuid()),
            TestCarId,
            TestWorkerId,
            StartDate,
            earlierFinish,
            OrderPriority.Medium);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.InvalidPlannedFinishDate);
    }

    [Fact]
    public void ChangeStatus_ToSameStatus_ReturnsSuccess()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);

        // Act
        var result = order.ChangeStatus(OrderStatus.Pending);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(OrderStatus.Pending);
    }

    [Fact]
    public void ChangeStatus_FromClosed_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        order.ChangeStatus(OrderStatus.Closed);

        // Act
        var result = order.ChangeStatus(OrderStatus.Pending);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.CannotChangeClosedOrderStatus);
    }

    [Fact]
    public void ChangeStatus_ToCompleted_WithUnfinishedWorks_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        order.AddWork(
            new OrderWorkId(Guid.NewGuid()),
            new JobId(Guid.NewGuid()),
            StandardHours.Create(2m).Value,
            Money.Create(50m).Value,
            null,
            isProposed: false);

        // Act
        var result = order.ChangeStatus(OrderStatus.Completed);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.CannotCompleteWithUnfinishedWorks);
    }

    [Fact]
    public void ChangeStatus_ToCompleted_WithAllWorksCompleted_ReturnsSuccess()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var workId = new OrderWorkId(Guid.NewGuid());
        order.AddWork(
            workId,
            new JobId(Guid.NewGuid()),
            StandardHours.Create(2m).Value,
            Money.Create(50m).Value,
            null,
            isProposed: false);

        var work = order.Works[0];
        work.BeginWork(TestWorkerId);
        work.CompleteWork(StandardHours.Create(2m).Value);

        // Act
        var result = order.ChangeStatus(OrderStatus.Completed);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(OrderStatus.Completed);
    }

    [Fact]
    public void ChangePriority_WithNewPriority_ChangesPriority()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);

        // Act
        var result = order.ChangePriority(OrderPriority.High);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Priority.Should().Be(OrderPriority.High);
    }

    [Fact]
    public void SetPlannedFinishDate_WithValidDate_ReturnsSuccess()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var newDate = StartDate.AddDays(20);

        // Act
        var result = order.SetPlannedFinishDate(newDate);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.PlannedFinishDate.Should().Be(newDate);
    }

    [Fact]
    public void SetPlannedFinishDate_BeforeStartDate_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);

        // Act
        var result = order.SetPlannedFinishDate(StartDate.AddDays(-1));

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.InvalidPlannedFinishDate);
    }

    [Fact]
    public void AddWork_WhenPending_AddsWorkAndRecalculatesAmount()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var workId = new OrderWorkId(Guid.NewGuid());
        var jobId = new JobId(Guid.NewGuid());

        // Act
        var result = order.AddWork(
            workId,
            jobId,
            StandardHours.Create(2m).Value,
            Money.Create(100m).Value,
            null,
            isProposed: false);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Works.Should().HaveCount(1);
        order.Works[0].Id.Should().Be(workId);
        // Work is pending, so not yet included in Amount
        order.Amount.Value.Should().Be(0m);
    }

    [Fact]
    public void AddWork_WhenCompleted_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        order.ChangeStatus(OrderStatus.Completed);

        // Act
        var result = order.AddWork(
            new OrderWorkId(Guid.NewGuid()),
            new JobId(Guid.NewGuid()),
            StandardHours.Create(1m).Value,
            Money.Create(100m).Value,
            null,
            isProposed: false);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.CannotAddWorkToClosedOrCompleted);
    }

    [Fact]
    public void RemoveWork_WhenPending_RemovesWork()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var workId = new OrderWorkId(Guid.NewGuid());
        order.AddWork(workId, new JobId(Guid.NewGuid()), StandardHours.Create(1m).Value, Money.Create(100m).Value, null, false);

        // Act
        var result = order.RemoveWork(workId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Works.Should().BeEmpty();
    }

    [Fact]
    public void RemoveWork_WhenInProgress_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var workId = new OrderWorkId(Guid.NewGuid());
        order.AddWork(workId, new JobId(Guid.NewGuid()), StandardHours.Create(1m).Value, Money.Create(100m).Value, null, false);
        order.Works[0].BeginWork(TestWorkerId);

        // Act
        var result = order.RemoveWork(workId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.CannotRemoveInProgressOrCompletedWork);
    }

    [Fact]
    public void RemoveWork_WhenNotFound_ReturnsSuccess()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);

        // Act
        var result = order.RemoveWork(new OrderWorkId(Guid.NewGuid()));

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void AddPart_WhenPending_AddsPartAndUpdatesAmount()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var partId = new OrderPartId(Guid.NewGuid());

        // Act
        var result = order.AddPart(
            partId,
            new PartId(Guid.NewGuid()),
            2m,
            Money.Create(150m).Value,
            isProposed: false);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Parts.Should().HaveCount(1);
        order.Amount.Value.Should().Be(300m); // 2 * 150
    }

    [Fact]
    public void AddPart_WhenClosed_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        order.ChangeStatus(OrderStatus.Closed);

        // Act
        var result = order.AddPart(
            new OrderPartId(Guid.NewGuid()),
            new PartId(Guid.NewGuid()),
            1m,
            Money.Create(100m).Value,
            isProposed: false);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.CannotAddPartToClosedOrCompleted);
    }

    [Fact]
    public void RemovePart_WhenExists_RemovesPartAndRecalculatesAmount()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var partId = new OrderPartId(Guid.NewGuid());
        order.AddPart(partId, new PartId(Guid.NewGuid()), 2m, Money.Create(150m).Value, false);
        order.Amount.Value.Should().Be(300m);

        // Act
        var result = order.RemovePart(partId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Parts.Should().BeEmpty();
        order.Amount.Value.Should().Be(0m);
    }

    [Fact]
    public void AddGuarantee_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var workId = new OrderWorkId(Guid.NewGuid());
        order.AddWork(workId, new JobId(Guid.NewGuid()), StandardHours.Create(1m).Value, Money.Create(100m).Value, null, false);
        var guaranteeId = new OrderGuaranteeId(Guid.NewGuid());

        // Act
        var result = order.AddGuarantee(
            guaranteeId,
            orderPartId: null,
            orderWorkId: workId,
            StartDate,
            FinishDate,
            "Work guarantee",
            "Full warranty");

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Guarantees.Should().HaveCount(1);
        order.Guarantees[0].Id.Should().Be(guaranteeId);
    }

    [Fact]
    public void AddGuarantee_WhenPartDoesNotBelongToOrder_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var nonExistentPartId = new OrderPartId(Guid.NewGuid());

        // Act
        var result = order.AddGuarantee(
            new OrderGuaranteeId(Guid.NewGuid()),
            nonExistentPartId,
            null,
            StartDate,
            FinishDate,
            null,
            "Terms");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.PartNotBelongToOrder);
    }

    [Fact]
    public void AddGuarantee_WhenWorkDoesNotBelongToOrder_ReturnsFailure()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var nonExistentWorkId = new OrderWorkId(Guid.NewGuid());

        // Act
        var result = order.AddGuarantee(
            new OrderGuaranteeId(Guid.NewGuid()),
            null,
            nonExistentWorkId,
            StartDate,
            FinishDate,
            null,
            "Terms");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Order.Errors.WorkNotBelongToOrder);
    }

    [Fact]
    public void RemoveGuarantee_WhenExists_RemovesGuarantee()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);
        var guaranteeId = new OrderGuaranteeId(Guid.NewGuid());
        order.AddGuarantee(guaranteeId, null, null, StartDate, FinishDate, null, "Terms");

        // Act
        var result = order.RemoveGuarantee(guaranteeId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        order.Guarantees.Should().BeEmpty();
    }

    [Fact]
    public void Amount_IncludesOnlyCompletedNonProposedWorksAndNonProposedParts()
    {
        // Arrange
        var order = OrderMother.CreatePending(carId: TestCarId, workerId: TestWorkerId, startDate: StartDate, finishDate: FinishDate, priority: OrderPriority.Medium);

        // Work 1: Completed, non-proposed -> 2 hours * 50 = 100
        var work1Id = new OrderWorkId(Guid.NewGuid());
        order.AddWork(work1Id, new JobId(Guid.NewGuid()), StandardHours.Create(2m).Value, Money.Create(50m).Value, null, false);
        order.Works[0].BeginWork(TestWorkerId);
        order.Works[0].CompleteWork(StandardHours.Create(2m).Value);

        // Work 2: Pending, non-proposed -> should not be included
        var work2Id = new OrderWorkId(Guid.NewGuid());
        order.AddWork(work2Id, new JobId(Guid.NewGuid()), StandardHours.Create(3m).Value, Money.Create(50m).Value, null, false);

        // Work 3: Completed, but PROPOSED -> should not be included
        var work3Id = new OrderWorkId(Guid.NewGuid());
        order.AddWork(work3Id, new JobId(Guid.NewGuid()), StandardHours.Create(1m).Value, Money.Create(50m).Value, null, true);

        // Part 1: Non-proposed -> 3 * 40 = 120
        var part1Id = new OrderPartId(Guid.NewGuid());
        order.AddPart(part1Id, new PartId(Guid.NewGuid()), 3m, Money.Create(40m).Value, false);

        // Part 2: Proposed -> should not be included
        var part2Id = new OrderPartId(Guid.NewGuid());
        order.AddPart(part2Id, new PartId(Guid.NewGuid()), 5m, Money.Create(100m).Value, true);

        // Assert: 100 (work1) + 120 (part1) = 220
        order.Amount.Value.Should().Be(220m);
    }
}
