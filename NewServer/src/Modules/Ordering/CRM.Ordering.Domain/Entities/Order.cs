using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities;

public sealed class Order : AggregateRoot<OrderId>, ISoftDeletable, IAuditable, IHasVersion
{
    private Order(
        OrderId id,
        OrderStatus status,
        CarId carId,
        WorkerId workerId,
        DateOnly startedAt,
        DateOnly? planedFinishDate,
        OrderPriority priority)
    {
        Id = id;
        Status = status;
        CarId = carId;
        WorkerId = workerId;
        StartedAt = startedAt;
        PlanedFinishDate = planedFinishDate;
        Priority = priority;
    }

#pragma warning disable CS8618
    private Order() { }
#pragma warning restore CS8618

    private readonly List<OrderWork> _works = [];

    private readonly List<OrderPart> _parts = [];

    private readonly List<OrderGuarantee> _guarantees = [];

    public OrderStatus Status { get; private set; }
    public CarId CarId { get; private set; }
    public WorkerId WorkerId { get; private set; }
    public DateOnly StartedAt { get; private set; }
    public DateOnly? PlanedFinishDate { get; private set; }
    public OrderPriority Priority { get; private set; }
    public Money? Ammount { get; private set; }

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
        OrderStatus status,
        CarId carId,
        WorkerId workerId,
        DateOnly startedAt,
        DateOnly? planedFinishDate,
        OrderPriority priority)
    {
        if (planedFinishDate.HasValue &&
            startedAt < planedFinishDate.Value)
        {
            return Result<Order>.Failure(Error.Validation<Order>(
                "Planed finish date should be less then start date."));
        }

        Order order = new(
            id,
            status,
            carId,
            workerId,
            startedAt,
            planedFinishDate,
            priority);

        return Result<Order>.Success(order);
    }

    public Result ChangeStatus(OrderStatus newStatus)
    {
        if (Status == newStatus)
        {
            return Result.Success();
        }

        return Result.Success();
    }

    public Result ChangePriority(OrderPriority newPriority)
    {
        if (Priority == newPriority)
        {
            return Result.Success();
        }

        return Result.Success();
    }

    public Result SetPlanedFinishDate(DateOnly newPlanedFinishDate)
    {
        if (PlanedFinishDate.HasValue &&
            newPlanedFinishDate == PlanedFinishDate.Value)
        {
            return Result.Success();
        }

        return Result.Success();
    }

    public Result CalculateAmount()
    {
        return Result.Success();
    }
}
