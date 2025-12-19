namespace CRMSystem.Core.DTOs.Worker;

public record WorkerFilter
(
    IEnumerable<int> workerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
