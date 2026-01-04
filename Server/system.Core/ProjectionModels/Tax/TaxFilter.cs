namespace CRMSystem.Core.DTOs.Tax;

public record TaxFilter
(
    IEnumerable<int> TaxTyprIds,
    string? SortBy,
    bool IsDescending
);
