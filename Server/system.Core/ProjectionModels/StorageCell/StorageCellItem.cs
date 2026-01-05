namespace CRMSystem.Core.DTOs.StorageCell;

public record StorageCellItem
{
    public int Id { get; init; }
    public string Rack { get; init; } = string.Empty;
    public string Shelf { get; init; } = string.Empty;
};
