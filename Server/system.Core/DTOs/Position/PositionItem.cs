namespace CRMSystem.Core.DTOs.Position;

public record PositionItem
(
    long Id,
    string Part,
    long PartId,
    int CellId,
    decimal PurchasePrice,
    decimal SellingPrice,
    decimal Quantity
);