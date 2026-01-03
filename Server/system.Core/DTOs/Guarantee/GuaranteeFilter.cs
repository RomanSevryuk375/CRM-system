namespace CRMSystem.Core.DTOs.Guarantee;

public record GuaranteeFilter
(
    IEnumerable<long> OrderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
