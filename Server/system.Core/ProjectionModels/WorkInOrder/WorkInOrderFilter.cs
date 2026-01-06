namespace CRMSystem.Core.ProjectionModels.WorkInOrder;

public record WorkInOrderFilter
(
    IEnumerable<long> OrderIds,
    IEnumerable<long> JobIds,
    IEnumerable<long> WorkerIds,
    IEnumerable<int> StatusIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
