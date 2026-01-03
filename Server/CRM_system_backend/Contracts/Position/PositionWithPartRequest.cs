namespace CRM_system_backend.Contracts.Position;

public record PositionWithPartRequest
(
    long Id,
    long PartId,
    int CellId,
    decimal PurchasePrice,
    decimal SellingPrice,
    decimal Quantity,
    int CategoryId,
    string? OemArticle,
    string? ManufacturerArticle,
    string InternalArticle,
    string? Description,
    string Name,
    string Manufacturer,
    string Applicability
);