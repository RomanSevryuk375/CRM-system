namespace Shared.Filters;

public record SupplyFilter
(
    IEnumerable<long>? SuplierIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
