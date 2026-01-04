namespace CRMSystem.Core.DTOs.SupplySet;

public record SupplySetFilter
(
    IEnumerable<long> SupplyIds,
    IEnumerable<long> PositionIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
