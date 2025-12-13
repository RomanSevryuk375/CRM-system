namespace CRMSystem.Core.DTOs.Acceptance;

public record AcceptanceFilter
(
    IEnumerable<int> workerIds,
    IEnumerable<long> orderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
