namespace CRMSystem.Core.ProjectionModels.Position;

public record PositionItem
{
    public long Id { get; init; }
    public string Part { get; init; } = string.Empty;
    public long PartId { get; init; }
    public int CellId { get; init; }
    public decimal PurchasePrice { get; init; }
    public decimal SellingPrice { get; init; }
    public decimal Quantity { get; init; }
};