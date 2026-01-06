namespace CRMSystem.Core.ProjectionModels.Order;

public record OrderFilter
(
    IEnumerable<long>? OrderIds,
    IEnumerable<int>? StatusIds,
    IEnumerable<int>? PriorityIds,
    IEnumerable<long>? CarIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
