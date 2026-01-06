namespace CRMSystem.Core.ProjectionModels.Work;

public record WorkFilter
(
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
