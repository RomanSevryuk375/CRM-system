using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Notification;

public record NotificationRequest
(
    long clientId,
    long carId,
    NotificationTypeEnum typeId,
    NotificationStatusEnum statusId,
    string message,
    DateTime sendAt
);
