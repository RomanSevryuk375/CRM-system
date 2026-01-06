namespace CRMSystem.Core.ProjectionModels.Bill;

public record BillFilter
(
    IEnumerable<long> OrderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
