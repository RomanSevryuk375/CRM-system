using CRM.Billing.Domain.ValueObjects;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;

namespace CRM.Billing.Domain.Entities;

public sealed class PriceListItem : IEntity<PriceListItemId>
{
    internal PriceListItem(
        PriceListItemId listItemId,
        PriceListId priceListId,
        JobId jobId,
        Money fixedPrice)
    {
        Id = listItemId;
        PriceListId = priceListId;
        JobId = jobId;
        FixedPrice = fixedPrice;
    }

    public PriceListItemId Id { get; private set; }
    public PriceListId PriceListId { get; private set; }
    public JobId JobId { get; private set; }
    public Money FixedPrice { get; private set; }

    internal void UpdatePrice(Money newPrice)
    {
        FixedPrice = newPrice;
    }
}