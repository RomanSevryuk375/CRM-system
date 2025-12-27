namespace CRMSystem.Core.DTOs.Guarantee;

public record GuaranteeFilter
(
    IEnumerable<long> orderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
