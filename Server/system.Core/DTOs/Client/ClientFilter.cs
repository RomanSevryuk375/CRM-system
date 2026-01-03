namespace CRMSystem.Core.DTOs.Client;

public record ClientFilter
(
    IEnumerable<long> UserIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
