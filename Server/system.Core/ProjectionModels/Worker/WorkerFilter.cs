namespace CRMSystem.Core.DTOs.Worker;

public record WorkerFilter
(
    IEnumerable<int> WorkerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
