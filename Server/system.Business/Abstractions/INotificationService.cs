using CRMSystem.Core.ProjectionModels.Notification;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface INotificationService
{
    Task<long> CreateNotification(NotificationCreateModel createModel, CancellationToken ct);
    Task<long> DeleteNotification(long id, CancellationToken ct);
    Task<int> GetCountNotifications(NotificationFilter filter, CancellationToken ct);
    Task<NotificationItem> GetNotificationById(long id, CancellationToken ct);
    Task<List<NotificationItem>> GetPagedNotifications(NotificationFilter filter, CancellationToken ct);
}