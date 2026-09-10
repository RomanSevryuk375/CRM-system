using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.Orders;

public sealed class OrderPart : IEntity<OrderPartId>
{
    public const decimal MinQuantity = 0m;
    public const int QuantityPrecision = 18;
    public const int QuantityScale = 3;

    internal OrderPart(
        OrderPartId id,
        OrderId orderId,
        PartId partId,
        bool isProposed,
        decimal quantity,
        Money soldPrice)
    {
        Id = id;
        OrderId = orderId;
        PartId = partId;
        IsProposed = isProposed;
        Quantity = quantity;
        SoldPrice = soldPrice;
    }


#pragma warning disable CS8618
    private OrderPart() { }
#pragma warning restore CS8618

    public OrderPartId Id { get; private set; }
    public OrderId OrderId { get; private set; }
    public PartId PartId { get; private set; }
    public bool IsProposed { get; private set; }

    public decimal Quantity { get; private set; }
    public Money SoldPrice { get; private set; }

    internal static Result<OrderPart> Create(
        OrderPartId id,
        OrderId orderId,
        PartId partId,
        decimal quantity,
        Money soldPrice,
        bool isProposed = false)
    {
        List<Error> errors = [];

        if (quantity < MinQuantity)
        {
            errors.Add(Error.Validation<OrderPart>(Errors.NegativeQuantity));
        }

        if (errors.Count != 0)
        {
            return Result<OrderPart>.Failure(Error.Validation<OrderPart>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        OrderPart part = new(
            id,
            orderId,
            partId,
            isProposed,
            quantity,
            soldPrice);

        return Result<OrderPart>.Success(part);
    }

    internal Result MarkAsProposed()
    {
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

    internal Result SetQuantity(decimal newQuantity)
    {
        if (newQuantity < MinQuantity)
        {
            return Result.Failure(Error.Validation<OrderPart>(Errors.NegativeQuantity));
        }

        Quantity = newQuantity;

        return Result.Success();
    }

    internal Result SetSoldPrice(Money newSoldPrice)
    {
        SoldPrice = newSoldPrice;

        return Result.Success();
    }

    public static class Errors
    {
        public const string NegativeQuantity = "Part quantity should be positive.";
    }
}
