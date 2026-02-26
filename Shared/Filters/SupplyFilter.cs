namespace Shared.Filters;

public record SupplyFilter
(
    IEnumerable<long>? SupplierIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
