namespace CRMSystem.Core.DTOs.SupplySet;

public record SupplySetItem
(
    long Id,
    long SupplyId,
    string Position,
    long PositionId,
    decimal Quantity,
    decimal PurchasePrice
);
