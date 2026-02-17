using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.NotificationType;

namespace CRMSystem.Business.Abstractions;

public interface INotificationTypeService
{
    Task<List<NotificationTypeItem>> GetNotificationTypes(CancellationToken ct);
}