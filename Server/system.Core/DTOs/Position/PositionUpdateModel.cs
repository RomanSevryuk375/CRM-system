namespace CRMSystem.Core.DTOs.Position;

public record PositionUpdateModel
(
    int? CellId,
    decimal? PurchasePrice,
    decimal? SellingPrice,
    decimal? Quantity
);
