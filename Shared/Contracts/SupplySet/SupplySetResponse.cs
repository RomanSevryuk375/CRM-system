namespace Shared.Contracts.SupplySet;

public record SupplySetResponse
(
    long Id,
    long SupplyId,
    string Position,
    long PositionId,
    decimal Quantity,
    decimal PurchasePrice
);
