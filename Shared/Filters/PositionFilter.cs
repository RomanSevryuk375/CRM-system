namespace Shared.Filters;

public record PositionFilter
(
    IEnumerable<long>? PartIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
