namespace Shared.Contracts.SupplySet;

public record SupplySetRequest
(
    long Id,
    long SupplyId,
    long PositionId,
    decimal Quantity,
    decimal PurchasePrice
);