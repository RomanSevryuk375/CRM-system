using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Domain.Entities;

public sealed class PriceList : AggregateRoot<PriceListId>, ISoftDeletable, IAuditable
{
    private readonly List<PriceListItem> _items = [];

    private PriceList(
        PriceListId id,
        Name name,
        DateOnly validFrom,
        DateOnly? validTo,
        bool isDefault)
    {
        Id = id;
        Name = name;
        ValidFrom = validFrom;
        ValidTo = validTo;
        IsDefault = isDefault;
    }

#pragma warning disable CS8618
    private PriceList() { }
#pragma warning restore CS8618

    public Name Name { get; private set; }
    public DateOnly ValidFrom { get; private set; }
    public DateOnly? ValidTo { get; private set; }
    public bool IsDefault { get; private set; }
    public Money BaseHourlyRate { get; private set; } = Money.Zero();

    public IReadOnlyList<PriceListItem> Items => _items.AsReadOnly();

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<PriceList> Create(
        PriceListId id,
        Name name,
        DateOnly validFrom,
        decimal baseHourlyRate)
    {
        Result<Money> baseHourlyRateResult = Money.Create(baseHourlyRate);
        if (baseHourlyRateResult.IsFailure)
        {
            return Result<PriceList>.Failure(baseHourlyRateResult.Error);
        }

        PriceList priceList = new(
            id,
            name,
            validFrom,
            validTo: null,
            isDefault: false)
        {
            BaseHourlyRate = baseHourlyRateResult.Value
        };

        priceList.IncrementVersion();

        return Result<PriceList>.Success(priceList);
    }

    public Result SetFixedPriceForJob(PriceListItemId listItemId, JobId jobId, decimal fixedPrice)
    {
        Result<Money> fixedPriceResult = Money.Create(fixedPrice);
        if (fixedPriceResult.IsFailure)
        {
            return Result.Failure(fixedPriceResult.Error);
        }

        PriceListItem? existingItem = _items.Find(i => i.JobId.Id == jobId.Id);
        if (existingItem is not null)
        {
            existingItem.UpdatePrice(fixedPriceResult.Value);

            return Result.Success();
        }

        _items.Add(new PriceListItem(
            listItemId,
            priceListId: Id,
            jobId,
            fixedPriceResult.Value));

        IncrementVersion();

        return Result.Success();
    }

    public Result Deactivate(DateOnly validTo)
    {
        if (validTo < ValidFrom)
        {
            return Result.Failure(Error.Validation<PriceList>(Errors.InvalidDeactivationDate));
        }

        if (ValidTo.HasValue && ValidTo.Value < validTo)
        {
            return Result.Failure(Error.Conflict<PriceList>(Errors.AlreadyDeactivatedEarlier));
        }

        ValidTo = validTo;
        if (IsDefault)
        {
            IsDefault = false;
        }

        IncrementVersion();

        return Result.Success();
    }

    public void MakeDefault()
    {
        if (IsDefault)
        {
            return;
        }

        IsDefault = true;
    }

    public bool IsValidOn(DateOnly date)
    {
        if (date < ValidFrom)
        {
            return false;
        }

        if (ValidTo.HasValue && date > ValidTo.Value)
        {
            return false;
        }

        return true;
    }

    public static class Errors
    {
        public const string InvalidDeactivationDate = "Deactivation date cannot be earlier than the activation date (ValidFrom).";
        public const string AlreadyDeactivatedEarlier = "The price list is already deactivated with an earlier date.";
    }
}