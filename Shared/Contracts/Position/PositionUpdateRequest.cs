namespace Shared.Contracts.Position;

public record PositionUpdateRequest
(
    int? CellId,
    decimal? PurchasePrice,
    decimal? SellingPrice,
    decimal? Quantity
);
