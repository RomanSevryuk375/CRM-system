using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.Orders;

public sealed class Order : AggregateRoot<OrderId>, ISoftDeletable, IAuditable, IHasVersion
{
    private readonly List<OrderWork> _works = [];
    private readonly List<OrderPart> _parts = [];
    private readonly List<OrderGuarantee> _guarantees = [];

    private Order(
        OrderId id,
        OrderStatus status,
        CarId carId,
        WorkerId workerId,
        DateOnly startedAt,
        DateOnly? plannedFinishDate,
        OrderPriority priority)
    {
        Id = id;
        Status = status;
        CarId = carId;
        WorkerId = workerId;
        StartedAt = startedAt;
        PlannedFinishDate = plannedFinishDate;
        Priority = priority;
        Amount = Money.Zero();
    }

#pragma warning disable CS8618
    private Order() { }
#pragma warning restore CS8618

    public OrderStatus Status { get; private set; }
    public CarId CarId { get; private set; }
    public WorkerId WorkerId { get; private set; }
    public DateOnly StartedAt { get; private set; }
    public DateOnly? PlannedFinishDate { get; private set; }
    public OrderPriority Priority { get; private set; }
    public Money Amount { get; private set; }

    public IReadOnlyList<OrderWork> Works => _works.AsReadOnly();
    public IReadOnlyList<OrderPart> Parts => _parts.AsReadOnly();
    public IReadOnlyList<OrderGuarantee> Guarantees => _guarantees.AsReadOnly();


#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public Guid Version { get; private set; }
#pragma warning restore S1144

    public static Result<Order> Create(
        OrderId id,
        CarId carId,
        WorkerId workerId,
        DateOnly startedAt,
        DateOnly? plannedFinishDate,
        OrderPriority priority)
    {
        if (plannedFinishDate.HasValue && startedAt > plannedFinishDate.Value)
        {
            return Result<Order>.Failure(Error.Validation<Order>(
                "Planned finish date cannot be earlier than start date."));
        }

        Order order = new(
            id,
            OrderStatus.Pending,
            carId,
            workerId,
            startedAt,
            plannedFinishDate,
            priority);

        order.IncrementVersion();

        return Result<Order>.Success(order);
    }

    public Result ChangeStatus(OrderStatus newStatus)
    {
        if (Status == newStatus)
        {
            return Result.Success();
        }

        if (Status is OrderStatus.Closed)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot change status of a closed order."));
        }

        if (ImpossibleToComplete(newStatus))
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot complete order because it contains unfinished works."));
        }

        Status = newStatus;
        IncrementVersion();

        return Result.Success();
    }

    public Result ChangePriority(OrderPriority newPriority)
    {
        if (Priority == newPriority)
        {
            return Result.Success();
        }

        Priority = newPriority;
        IncrementVersion();

        return Result.Success();
    }

    public Result SetPlannedFinishDate(DateOnly newPlannedFinishDate)
    {
        if (newPlannedFinishDate < StartedAt)
        {
            return Result.Failure(Error.Validation<Order>(
                "Planned finish date cannot be earlier than start date."));
        }

        PlannedFinishDate = newPlannedFinishDate;
        IncrementVersion();

        return Result.Success();
    }

    public Result AddWork(
        OrderWorkId orderWorkId,
        JobId jobId,
        decimal estimatedHours,
        decimal? hourlyRate,
        decimal? fixedPrice,
        bool isProposed)
    {
        if (Status is OrderStatus.Completed ||
            Status is OrderStatus.Closed)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot add works to completed or closed orders."));
        }

        Result<OrderWork> workResult = OrderWork.Create(
            orderWorkId,
            orderId: Id,
            jobId,
            status: WorkStatus.Pending,
            hourlyRate,
            estimatedHours,
            fixedPrice,
            isProposed);
        if (workResult.IsFailure)
        {
            return workResult;
        }

        _works.Add(workResult.Value);

        RecalculateAmount();
        IncrementVersion();

        return Result.Success();
    }

    public Result RemoveWork(OrderWorkId orderWorkId)
    {
        if (Status is OrderStatus.Completed ||
            Status is OrderStatus.Closed)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot remove works from completed or closed orders."));
        }

        OrderWork? work = _works.Find(w => w.Id == orderWorkId);
        if (work is null)
        {
            return Result.Success();
        }

        if (work.Status is not WorkStatus.Pending)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot remove a work that is already in progress or completed."));
        }

        _works.Remove(work);

        RecalculateAmount();
        IncrementVersion();

        return Result.Success();
    }

    public Result AddPart(
        OrderPartId orderPartId,
        PartId partId,
        decimal quantity,
        decimal soldPrice,
        bool isProposed)
    {
        if (Status is OrderStatus.Completed ||
            Status is OrderStatus.Closed)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot add parts to completed or closed orders."));
        }

        Result<OrderPart> partResult = OrderPart.Create(
            orderPartId,
            orderId: Id,
            partId,
            quantity,
            soldPrice,
            isProposed);
        if (partResult.IsFailure)
        {
            return partResult;
        }

        _parts.Add(partResult.Value);

        RecalculateAmount();
        IncrementVersion();

        return Result.Success();
    }

    public Result RemovePart(OrderPartId orderPartId)
    {
        if (Status is OrderStatus.Completed ||
            Status is OrderStatus.Closed)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot remove parts from completed or closed orders."));
        }

        OrderPart? part = _parts.Find(p => p.Id == orderPartId);
        if (part is null)
        {
            return Result.Success();
        }

        _parts.Remove(part);

        RecalculateAmount();
        IncrementVersion();

        return Result.Success();
    }

    public Result AddGuarantee(
        OrderGuaranteeId orderGuaranteeId,
        OrderPartId? orderPartId,
        OrderWorkId? orderWorkId,
        DateOnly dateStart,
        DateOnly dateEnd,
        string? description,
        string terms)
    {
        if (Status is OrderStatus.Closed)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot add guarantees to closed orders."));
        }

        if (orderPartId is not null &&
            !_parts.Exists(p => p.Id == orderPartId))
        {
            return Result.Failure(Error.NotFound<Order>(
                "The specified part does not belong to this order."));
        }

        if (orderWorkId is not null &&
            !_works.Exists(w => w.Id == orderWorkId))
        {
            return Result.Failure(Error.NotFound<Order>(
                "The specified work does not belong to this order."));
        }

        Result<OrderGuarantee> guaranteeResult = OrderGuarantee.Create(
            orderGuaranteeId,
            orderId: Id,
            orderPartId,
            orderWorkId,
            dateStart,
            dateEnd,
            description,
            terms);
        if (guaranteeResult.IsFailure)
        {
            return guaranteeResult;
        }

        _guarantees.Add(guaranteeResult.Value);
        IncrementVersion();

        return Result.Success();
    }

    public Result RemoveGuarantee(OrderGuaranteeId orderGuaranteeId)
    {
        if (Status is OrderStatus.Closed)
        {
            return Result.Failure(Error.Conflict<Order>(
                "Cannot remove guarantees from a closed order."));
        }

        OrderGuarantee? guarantee = _guarantees.Find(g => g.Id == orderGuaranteeId);
        if (guarantee is null)
        {
            return Result.Success();
        }

        _guarantees.Remove(guarantee);
        IncrementVersion();

        return Result.Success();
    }

    private void RecalculateAmount()
    {
        decimal total = 0m;

        decimal completedWorksCost = _works
            .Where(w => !w.IsProposed &&
                         w.Status is WorkStatus.Completed)
            .Sum(w => w.TotalCost?.Value ?? 0m);

        decimal partsCost = _parts
            .Where(p => !p.IsProposed)
            .Sum(p => p.SoldPrice.Value * p.Quantity);

        total = completedWorksCost + partsCost;

        Result<Money> moneyResult = Money.Create(total);
        if (moneyResult.IsSuccess)
        {
            Amount = moneyResult.Value;
        }
    }

    private bool ImpossibleToComplete(OrderStatus newStatus)
    {
        return newStatus is OrderStatus.Completed &&
               _works.Exists(w => w.Status is not WorkStatus.Completed &&
                                 !w.IsProposed);
    }

    private void IncrementVersion()
    {
        Version = Guid.NewGuid();
    }
}