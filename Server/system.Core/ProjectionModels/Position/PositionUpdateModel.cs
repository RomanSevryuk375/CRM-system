namespace CRMSystem.Core.ProjectionModels.Position;

public record PositionUpdateModel
(
    int? CellId,
    decimal? PurchasePrice,
    decimal? SellingPrice,
    decimal? Quantity
);
