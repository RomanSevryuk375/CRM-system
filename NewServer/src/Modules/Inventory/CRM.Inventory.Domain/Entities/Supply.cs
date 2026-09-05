using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class Supply : AggregateRoot<SupplyId>, IAuditable, ISoftDeletable
{
    private readonly List<SupplyItem> _items = [];

    private Supply(SupplyId id, SupplierId supplierId, DateOnly date)
    {
        Id = id;
        SupplierId = supplierId;
        Date = date;
    }

#pragma warning disable CS8618
    private Supply() { }
#pragma warning restore CS8618

    public SupplierId SupplierId { get; private set; }
    public DateOnly Date { get; private set; }

    public IReadOnlyList<SupplyItem> Items => _items.AsReadOnly();

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Supply> Create(SupplyId id, SupplierId supplierId, DateOnly date)
    {
        Supply supply = new(id, supplierId, date);
        supply.IncrementVersion();

        return Result<Supply>.Success(supply);
    }

    public Result AddItem(SupplyItemId itemId, PositionId positionId, decimal quantity, Money price)
    {
        if (quantity <= 0)
        {
            return Result.Failure(Error.Validation<Supply>(Errors.InvalidQuantity));
        }

        _items.Add(SupplyItem.Create(itemId, Id, positionId, quantity, price).Value);
        IncrementVersion();

        return Result.Success();
    }

    public Result RemoveItem(SupplyItemId itemId)
    {
        SupplyItem? item = _items.Find(x => x.Id == itemId);
        if (item == null)
        {
            return Result.Success();
        }

        _items.Remove(item);
        IncrementVersion();

        return Result.Success();
    }

    public static class Errors
    {
        public const string InvalidQuantity = "Quantity must be greater than zero.";
    }
}
