namespace CRMSystem.Core.DTOs.Supply;

public record SupplyItem
{
    public long Id { get; init; }
    public string Supplier { get; init; } = string.Empty;
    public int SupplierId { get; init; }
    public DateOnly Date { get; init; }
};
