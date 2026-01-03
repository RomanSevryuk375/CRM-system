using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Notification;

public record NotificationRequest
(
    long ClientId,
    long CarId,
    NotificationTypeEnum TypeId,
    NotificationStatusEnum StatusId,
    string Message,
    DateTime SendAt
);
