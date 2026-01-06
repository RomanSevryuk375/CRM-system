namespace CRMSystem.Core.ProjectionModels.Part;

public record PartFilter
(
    IEnumerable<long> CategoryIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
