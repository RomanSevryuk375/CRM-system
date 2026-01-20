namespace Shared.Contracts.StorageCell;

public record StorageCellUpdateRequest
(
    string? Rack,
    string? Shelf
);