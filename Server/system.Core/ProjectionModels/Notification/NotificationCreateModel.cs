using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.Notification;

public record NotificationCreateModel
(
    long ClientId,
    long CarId,
    NotificationTypeEnum TypeId,
    NotificationStatusEnum StatusId,
    string Message,
    DateTime SendAt);