using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Notification;

public record NotificationFilter
(
    IEnumerable<long> clientIds,
    IEnumerable<long> carIds,
    IEnumerable<NotificationTypeEnum> typeIds,
    IEnumerable<NotificationStatusEnum> statusIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
