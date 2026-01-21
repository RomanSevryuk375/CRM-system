namespace Shared.Filters;

public record WorkFilter
(
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
