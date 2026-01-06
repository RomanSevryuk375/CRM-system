namespace CRMSystem.Core.ProjectionModels.Absence;

public record AbsenceFilter
(
    IEnumerable<int>? WorkerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
