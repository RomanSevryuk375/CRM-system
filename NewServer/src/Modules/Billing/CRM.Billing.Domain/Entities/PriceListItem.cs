using CRM.Billing.Domain.ValueObjects;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;

namespace CRM.Billing.Domain.Entities;

public sealed class PriceListItem : IEntity<PriceListItemId>
{
    internal PriceListItem(
        PriceListItemId id,
        PriceListId priceListId,
        JobId jobId,
        Money fixedPrice)
    {
        Id = id;
        PriceListId = priceListId;
        JobId = jobId;
        FixedPrice = fixedPrice;
    }

#pragma warning disable CS8618 
    private PriceListItem() { }
#pragma warning restore CS8618

    public PriceListItemId Id { get; private set; }
    public PriceListId PriceListId { get; private set; }
    public JobId JobId { get; private set; }
    public Money FixedPrice { get; private set; }

    internal void UpdatePrice(Money newPrice)
    {
        FixedPrice = newPrice;
    }
}