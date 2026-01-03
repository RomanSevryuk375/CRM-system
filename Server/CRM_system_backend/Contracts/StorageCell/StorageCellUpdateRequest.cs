namespace CRM_system_backend.Contracts.StorageCell;

public record StorageCellUpdateRequest
(
    string? Rack,
    string? Shelf
);