namespace Shared.Filters;

public record OrderFilter
(
    IEnumerable<long>? OrderIds,
    IEnumerable<int>? StatusIds,
    IEnumerable<int>? PriorityIds,
    IEnumerable<long>? CarIds,
    IEnumerable<long>? ClientIds,
    IEnumerable<int>? WorkerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
