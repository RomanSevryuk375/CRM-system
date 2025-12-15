namespace CRMSystem.Core.DTOs.Client;

public record ClientFilter
(
    IEnumerable<long> userIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
