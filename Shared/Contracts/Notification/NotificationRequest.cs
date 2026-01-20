using Shared.Enums;

namespace Shared.Contracts.Notification;

public record NotificationRequest
(
    long ClientId,
    long CarId,
    NotificationTypeEnum TypeId,
    NotificationStatusEnum StatusId,
    string Message,
    DateTime SendAt
);
