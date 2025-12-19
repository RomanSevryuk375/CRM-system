using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Order;

public record OrderFilter
(
    IEnumerable<int> statusIds,
    IEnumerable<int> priorityIds,
    IEnumerable<long> carIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
