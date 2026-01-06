namespace CRMSystem.Core.ProjectionModels.SupplySet;

public record SupplySetItem
(
    long Id,
    long SupplyId,
    string Position,
    long PositionId,
    decimal Quantity,
    decimal PurchasePrice
);
