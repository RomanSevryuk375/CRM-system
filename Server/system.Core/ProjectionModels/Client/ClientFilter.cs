namespace CRMSystem.Core.ProjectionModels.Client;

public record ClientFilter
(
    IEnumerable<long> UserIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
