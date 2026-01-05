namespace CRM_system_backend.Contracts.StorageCell;

public record StorageCellResponse
{
    public int Id { get; init; }
    public string Rack { get; init; } = string.Empty;
    public string Shelf { get; init; } = string.Empty;
};
