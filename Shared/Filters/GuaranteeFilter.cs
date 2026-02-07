namespace Shared.Filters;

public record GuaranteeFilter
(
    IEnumerable<long>? OrderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
