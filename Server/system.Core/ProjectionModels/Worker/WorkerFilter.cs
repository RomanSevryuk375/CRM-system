namespace CRMSystem.Core.ProjectionModels.Worker;

public record WorkerFilter
(
    IEnumerable<int> WorkerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
