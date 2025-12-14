namespace CRMSystem.Core.DTOs.Bill;

public record BillFilter
(
    IEnumerable<long> OrderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
