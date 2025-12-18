using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Tax;

public record TaxFilter
(
    IEnumerable<TaxTypeEnum> taxTyprIds,
    string? SortBy,
    bool isDescending
);
