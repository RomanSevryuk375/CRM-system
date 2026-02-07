namespace Shared.Filters;

public record ClientFilter
(
    IEnumerable<long>? ClientIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
