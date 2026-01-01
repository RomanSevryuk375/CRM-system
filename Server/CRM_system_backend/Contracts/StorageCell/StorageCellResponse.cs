namespace CRM_system_backend.Contracts.StorageCell;

public record StorageCellResponse
(
    int id,
    string rack,
    string shelf
);
