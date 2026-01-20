namespace Shared.Contracts.Position;

public record PositionRequest
(
    long PartId,
    int CellId,
    decimal PurchasePrice,
    decimal SellingPrice,
    decimal Quantity
);
