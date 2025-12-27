using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkInOrder;

public record WorkInOrderFilter
(
    IEnumerable<long> orderIds,
    IEnumerable<long> jobIds,
    IEnumerable<long> workerIds,
    IEnumerable<int> statusIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
