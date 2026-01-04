namespace CRM_system_backend.Contracts.Position;

public record PositionResponse
{
    public long Id { get; init; }
    public string Part { get; init; } = string.Empty;
    public long PartId { get; init; }
    public int CellId { get; init; }
    public decimal PurchasePrice { get; init; }
    public decimal SellingPrice { get; init; }
    public decimal Quantity { get; init; }
};
