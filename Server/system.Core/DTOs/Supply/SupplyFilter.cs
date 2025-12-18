namespace CRMSystem.Core.DTOs.Supply;

public record SupplyFilter
(
    IEnumerable<long> suplierIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
