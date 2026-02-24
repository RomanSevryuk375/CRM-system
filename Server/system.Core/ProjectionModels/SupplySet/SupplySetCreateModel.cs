namespace CRMSystem.Core.ProjectionModels.SupplySet;

public record SupplySetCreateModel
(
    long SupplyId,
    long PositionId,
    decimal Quantity,
    decimal PurchasePrice);