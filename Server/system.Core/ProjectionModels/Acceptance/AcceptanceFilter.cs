namespace CRMSystem.Core.ProjectionModels.Acceptance;

public record AcceptanceFilter
(
    IEnumerable<long>? AcceptanceIds,
    IEnumerable<int>? WorkerIds,
    IEnumerable<long>? OrderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
