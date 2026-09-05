using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class Position : AggregateRoot<PositionId>, IAuditable, ISoftDeletable
{
    private Position(
        PositionId id,
        PartId partId,
        StorageCellId cellId,
        Money purchasePrice,
        Money sellingPrice,
        decimal quantity)
    {
        Id = id;
        PartId = partId;
        CellId = cellId;
        PurchasePrice = purchasePrice;
        SellingPrice = sellingPrice;
        Quantity = quantity;
    }

#pragma warning disable CS8618
    private Position() { }
#pragma warning restore CS8618

    public PartId PartId { get; private set; }
    public StorageCellId CellId { get; private set; }
    public Money PurchasePrice { get; private set; }
    public Money SellingPrice { get; private set; }
    public decimal Quantity { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Position> Create(
        PositionId id,
        PartId partId,
        StorageCellId cellId,
        Money purchasePrice,
        Money sellingPrice,
        decimal quantity)
    {
        if (quantity < 0)
        {
            return Result<Position>.Failure(Error.Validation<Position>(Errors.NegativeQuantity));
        }

        Position position = new(
            id,
            partId,
            cellId,
            purchasePrice,
            sellingPrice,
            quantity);
        position.IncrementVersion();

        return Result<Position>.Success(position);
    }

    public Result UpdateQuantity(decimal change)
    {
        if (Quantity + change < 0)
        {
            return Result.Failure(Error.Conflict<Position>(Errors.NotEnoughQuantity));
        }

        Quantity += change;
        IncrementVersion();

        return Result.Success();
    }

    public static class Errors
    {
        public const string NegativeQuantity = "Quantity cannot be negative.";
        public const string NotEnoughQuantity = "Not enough quantity in position.";
    }
}
