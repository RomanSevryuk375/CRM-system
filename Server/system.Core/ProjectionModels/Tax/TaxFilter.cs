namespace CRMSystem.Core.ProjectionModels.Tax;

public record TaxFilter
(
    IEnumerable<int> TaxTyprIds,
    string? SortBy,
    bool IsDescending
);
