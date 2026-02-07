namespace Shared.Filters;

public record TaxFilter
(
    IEnumerable<int>? TaxTyprIds,
    string? SortBy,
    bool IsDescending
);
