namespace Shared.Filters;

public record TaxFilter
(
    IEnumerable<int>? TaxTypeIds,
    string? SortBy,
    bool IsDescending
);
