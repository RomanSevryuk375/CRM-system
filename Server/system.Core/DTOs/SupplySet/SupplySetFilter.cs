namespace CRMSystem.Core.DTOs.SupplySet;

public record SupplySetFilter
(
    IEnumerable<long> supplyIds,
    IEnumerable<long> positionIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
