namespace Shared.Filters;

public record CarFilter
(
    IEnumerable<long>? OwnerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);

