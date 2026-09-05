using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.Orders;

public sealed class OrderWork : IEntity<OrderWorkId>
{
    internal OrderWork(
        OrderWorkId id,
        OrderId orderId,
        JobId jobId,
        WorkStatus status,
        StandardHours estimatedHours,
        Money? hourlyRate,
        Money? fixedPrice,
        Money? totalCost,
        bool isProposed)
    {
        Id = id;
        OrderId = orderId;
        JobId = jobId;
        Status = status;
        EstimatedHours = estimatedHours;
        HourlyRate = hourlyRate;
        FixedPrice = fixedPrice;
        TotalCost = totalCost;
        IsProposed = isProposed;
    }

#pragma warning disable CS8618
    private OrderWork() { }
#pragma warning restore CS8618

    public OrderWorkId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public JobId JobId { get; private set; }
    public WorkerId? WorkerId { get; private set; }
    public StandardHours EstimatedHours { get; private set; }
    public StandardHours? TimeSpent { get; private set; }

    public bool IsProposed { get; private set; }
    public WorkStatus Status { get; private set; }

    public Money? HourlyRate { get; private set; }
    public Money? FixedPrice { get; private set; }
    public Money? TotalCost { get; private set; }

    public static Result<OrderWork> Create(
        OrderWorkId id,
        OrderId orderId,
        JobId jobId,
        WorkStatus status,
        Money? hourlyRate,
        StandardHours estimatedHours,
        Money? fixedPrice,
        bool isProposed = false)
    {
        List<Error> errors = [];

        if (status == WorkStatus.Completed)
        {
            errors.Add(Error.Validation<OrderWork>(Errors.CannotBeCompleted));
        }

        if (hourlyRate is null && fixedPrice is null)
        {
            errors.Add(Error.Validation<OrderWork>(Errors.MissingPriceOrRate));
        }

        if (errors.Count != 0)
        {
            return Result<OrderWork>.Failure(Error.Validation<OrderWork>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        Money? totalCost = null;
        if (fixedPrice is not null)
        {
            totalCost = fixedPrice;
        }
        else
        {
            decimal calculatedAmount = estimatedHours.Value * hourlyRate!.Value;
            Result<Money> costResult = Money.Create(calculatedAmount);
            if (costResult.IsSuccess)
            {
                totalCost = costResult.Value;
            }
        }

        OrderWork work = new(
            id, orderId, jobId,
            status, estimatedHours,
            hourlyRate, fixedPrice, totalCost,
            isProposed);

        return Result<OrderWork>.Success(work);
    }

    internal Result MarkAsProposed()
    {
        if (Status is not WorkStatus.Pending)
        {
            return Result.Failure(Error.Conflict<OrderWork>(Errors.OnlyPendingCanBeProposed));
        }

        IsProposed = true;
        return Result.Success();
    }

    internal Result Approve()
    {
        if (!IsProposed)
        {
            return Result.Success();
        }

        IsProposed = false;
        return Result.Success();
    }

    internal Result BeginWork(WorkerId workerId)
    {
        if (Status is WorkStatus.Completed && IsProposed)
        {
            return Result.Failure(Error.Conflict<OrderWork>(Errors.CannotBeginCompletedOrProposed));
        }

        WorkerId = workerId;
        Status = WorkStatus.InProgress;

        return Result.Success();
    }

    internal Result CompleteWork(StandardHours timeSpent)
    {
        if (Status is WorkStatus.Completed)
        {
            return Result.Failure(Error.Conflict<OrderWork>(Errors.AlreadyCompleted));
        }

        if (WorkerId is null)
        {
            return Result.Failure(Error.Conflict<OrderWork>(Errors.WorkerNotAssigned));
        }

        TimeSpent = timeSpent;
        Status = WorkStatus.Completed;

        return Result.Success();
    }

    public static class Errors
    {
        public const string CannotBeCompleted = "A newly created work cannot be completed.";
        public const string MissingPriceOrRate = "Either HourlyRate or FixedPrice must be provided.";
        public const string OnlyPendingCanBeProposed = "Only pending works can be proposed.";
        public const string CannotBeginCompletedOrProposed = "Cannot begin a completed or proposed work.";
        public const string AlreadyCompleted = "Work is already completed.";
        public const string WorkerNotAssigned = "Cannot complete a work without assigned worker.";
    }
}