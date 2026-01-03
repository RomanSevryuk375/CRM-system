using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Notification;

public record NotificationItem
(
    long Id,
    string Client,
    long ClientId,
    string Car,
    long CarId,
    string Type,
    int TypeId,
    string Status,
    int StatusId,
    string Message,
    DateTime SendAt
);

