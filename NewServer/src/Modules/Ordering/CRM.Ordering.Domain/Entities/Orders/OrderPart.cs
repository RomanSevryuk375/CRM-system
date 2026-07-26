using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.Orders;

public sealed class OrderPart : IEntity<OrderPartId>
{
    private const int MinQuantity = 0;

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
        decimal soldPrice,
        bool isProposed = false)
    {
        List<Error> errors = [];

        if (quantity < MinQuantity)
        {
            errors.Add(Error.Validation<OrderPart>(
                "Part quantity should be positive."));
        }

        Result<Money> moneyResult = Money.Create(soldPrice);
        if (moneyResult.IsFailure)
        {
            errors.Add(moneyResult.Error);
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
            moneyResult.Value);

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
            return Result.Failure(Error.Validation<OrderPart>(
                "Part quantity should be positive."));
        }

        Quantity = newQuantity;

        return Result.Success();
    }

    internal Result SetSoldPrice(decimal newSoldPrice)
    {
        Result<Money> moneyResult = Money.Create(newSoldPrice);
        if (moneyResult.IsFailure)
        {
            return moneyResult;
        }

        SoldPrice = moneyResult.Value;

        return Result.Success();
    }
}
