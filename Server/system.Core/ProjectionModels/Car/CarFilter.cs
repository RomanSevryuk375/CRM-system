namespace CRMSystem.Core.ProjectionModels.Car;

public record CarFilter
(
    IEnumerable<long> OwnerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);

