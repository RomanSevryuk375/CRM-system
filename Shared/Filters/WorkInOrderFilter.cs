namespace Shared.Filters;

public record WorkInOrderFilter
(
    IEnumerable<long>? OrderIds,
    IEnumerable<long>? JobIds,
    IEnumerable<int>? WorkerIds,
    IEnumerable<int>? StatusIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
