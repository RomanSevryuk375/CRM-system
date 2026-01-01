namespace CRM_system_backend.Contracts.StorageCell;

public record StorageCellUpdateRequest
(
    string? rack,
    string? shelf
);