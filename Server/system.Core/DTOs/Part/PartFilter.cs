namespace CRMSystem.Core.DTOs.Part;

public record PartFilter
(
    IEnumerable<long> CategoryIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
