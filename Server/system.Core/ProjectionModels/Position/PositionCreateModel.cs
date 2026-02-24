namespace CRMSystem.Core.ProjectionModels.Position;

public record PositionCreateModel
(
    long PartId,
    int CellId,
    decimal PurchasePrice,
    decimal SellingPrice,
    decimal Quantity);