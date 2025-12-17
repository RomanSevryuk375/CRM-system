namespace CRMSystem.Core.DTOs.Position;

public record PositionUpdateModel
(
    int? cellId,
    decimal? purchasePrice,
    decimal? sellingPrice,
    decimal? quantity
);
