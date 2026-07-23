using CRM.Billing.Domain.ValueObjects;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Domain.Entities;

public sealed class PriceList : AggregateRoot<PriceListId>, ISoftDeletable, IAuditable
{
    private readonly List<PriceListItem> _items = [];

    private PriceList(
        PriceListId id,
        string name,
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

    public string Name { get; private set; }
    public DateOnly ValidFrom { get; private set; }
    public DateOnly? ValidTo { get; private set; }
    public bool IsDefault { get; private set; }
    public Money BaseHourlyRate { get; private set; } = Money.Zero();

    public IReadOnlyList<PriceListItem> Items => _items.AsReadOnly();

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }

    public static Result<PriceList> Create(
        PriceListId id,
        string name,
        DateOnly validFrom,
        Money baseHourlyRate)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<PriceList>.Failure(Error.Validation<PriceList>(
                "Name cannot be empty"));
        }

        PriceList priceList = new(
            id,
            name,
            validFrom,
            validTo: null,
            isDefault: false)
        {
            BaseHourlyRate = baseHourlyRate
        };

        return Result<PriceList>.Success(priceList);
    }

    public Result SetFixedPriceForJob(PriceListItemId listItemId, JobId jobId, Money fixedPrice)
    {
        PriceListItem? existingItem = _items.Find(i => i.JobId.Id == jobId.Id);
        if (existingItem is not null)
        {
            existingItem.UpdatePrice(fixedPrice);

            return Result.Success();
        }

        _items.Add(new PriceListItem(
            listItemId,
            priceListId: Id,
            jobId,
            fixedPrice));

        return Result.Success();
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
}