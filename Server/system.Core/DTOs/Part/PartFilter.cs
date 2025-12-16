namespace CRMSystem.Core.DTOs.Part;

public record PartFilter
(
    IEnumerable<long> categoryIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
