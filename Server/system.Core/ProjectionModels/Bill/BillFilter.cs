namespace CRMSystem.Core.ProjectionModels.Bill;

public record BillFilter
(
    IEnumerable<long> OrderIds,
    IEnumerable<long> ClientIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
