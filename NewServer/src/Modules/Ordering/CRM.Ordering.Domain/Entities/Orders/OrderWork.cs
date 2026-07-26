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
        Money? hourlyRate,
        Money? fixedPrice,
        bool isProposed)
    {
        Id = id;
        OrderId = orderId;
        JobId = jobId;
        Status = status;
        HourlyRate = hourlyRate;
        FixedPrice = fixedPrice;
        IsProposed = isProposed;
    }

#pragma warning disable CS8618
    private OrderWork() { }
#pragma warning restore CS8618

    public OrderWorkId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public JobId JobId { get; private set; }
    public WorkerId? WorkerId { get; private set; }
    public StandardHours? TimeSpent { get; private set; }

    public bool IsProposed { get; private set; }
    public WorkStatus Status { get; private set; }

    public Money? HourlyRate { get; private set; }
    public Money? FixedPrice { get; private set; }
    public Money? TotalCost { get; private set; }

    internal static Result<OrderWork> Create(
        OrderWorkId id,
        OrderId orderId,
        JobId jobId,
        WorkStatus status,
        decimal? hourlyRate,
        decimal? fixedPrice,
        bool isProposed = false)
    {
        List<Error> errors = [];

        if (status == WorkStatus.Completed)
        {
            errors.Add(Error.Validation<OrderWork>(
                "A newly created work cannot be completed."));
        }

        if (hourlyRate is null && fixedPrice is null)
        {
            errors.Add(Error.Validation<OrderWork>(
                "Either HourlyRate or FixedPrice must be provided."));
        }

        Money? rateMoney = null;
        if (hourlyRate.HasValue)
        {
            Result<Money> rateResult = Money.Create(hourlyRate.Value);
            if (rateResult.IsFailure)
            {
                errors.Add(rateResult.Error);
            }
            else
            {
                rateMoney = rateResult.Value;
            }
        }

        Money? fixedMoney = null;
        if (fixedPrice.HasValue)
        {
            Result<Money> fixedResult = Money.Create(fixedPrice.Value);
            if (fixedResult.IsFailure)
            {
                errors.Add(fixedResult.Error);
            }
            else
            {
                fixedMoney = fixedResult.Value;
            }
        }

        if (errors.Count != 0)
        {
            return Result<OrderWork>.Failure(Error.Validation<OrderWork>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        OrderWork work = new(
            id,
            orderId,
            jobId,
            status,
            rateMoney,
            fixedMoney,
            isProposed);

        return Result<OrderWork>.Success(work);
    }


    internal Result MarkAsProposed()
    {
        if (Status is not WorkStatus.Pending)
        {
            return Result.Failure(Error.Conflict<OrderWork>(
                "Only pending works can be proposed."));
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
        if (Status is WorkStatus.Completed)
        {
            return Result.Failure(Error.Conflict<OrderWork>(
                "Cannot begin a completed work."));
        }

        WorkerId = workerId;
        Status = WorkStatus.InProgress;
        IsProposed = false;

        return Result.Success();
    }

    internal Result CompleteWork(decimal timeSpent)
    {
        if (Status is WorkStatus.Completed)
        {
            return Result.Failure(Error.Conflict<OrderWork>(
                "Work is already completed."));
        }

        if (WorkerId is null)
        {
            return Result.Failure(Error.Conflict<OrderWork>(
                "Cannot complete a work without assigned worker."));
        }

        Result<StandardHours> timeResult = StandardHours.Create(timeSpent);
        if (timeResult.IsFailure)
        {
            return Result.Failure(timeResult.Error);
        }

        TimeSpent = timeResult.Value;
        Status = WorkStatus.Completed;

        if (FixedPrice is null)
        {
            decimal calculatedAmount = TimeSpent.Value * HourlyRate!.Value;
            TotalCost = Money.Create(calculatedAmount).Value;
        }
        else
        {
            TotalCost = FixedPrice;
        }

        return Result.Success();
    }
}