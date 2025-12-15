using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Order;

public record OrderFilter
(
    IEnumerable<OrderStatusEnum> statusIds,
    IEnumerable<OrderPriorityEnum> priorityIds,
    IEnumerable<long> carIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
