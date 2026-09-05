using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class SupplyItem : Entity<SupplyItemId>
{
    internal SupplyItem(
        SupplyItemId id,
        SupplyId supplyId,
        PositionId positionId,
        decimal quantity,
        Money price)
    {
        Id = id;
        SupplyId = supplyId;
        PositionId = positionId;
        Quantity = quantity;
        Price = price;
    }

#pragma warning disable CS8618
    private SupplyItem() { }
#pragma warning restore CS8618

    public SupplyId SupplyId { get; private set; }
    public PositionId PositionId { get; private set; }
    public decimal Quantity { get; private set; }
    public Money Price { get; private set; }

    internal static Result<SupplyItem> Create(
        SupplyItemId id,
        SupplyId supplyId,
        PositionId positionId,
        decimal quantity,
        Money price)
    {
        return Result<SupplyItem>.Success(
            new SupplyItem(id, supplyId, positionId, quantity, price));
    }
}
