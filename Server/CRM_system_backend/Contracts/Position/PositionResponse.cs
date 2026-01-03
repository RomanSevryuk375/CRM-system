namespace CRM_system_backend.Contracts.Position;

public record PositionResponse
(
    long Id,
    string Part,
    long PartId,
    int CellId,
    decimal PurchasePrice,
    decimal SellingPrice,
    decimal Quantity
);
