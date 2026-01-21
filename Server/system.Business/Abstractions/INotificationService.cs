using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface INotificationService
{
    Task<long> CreateNotification(Notification notification, CancellationToken ct);
    Task<long> DeleteNotification(long id, CancellationToken ct);
    Task<int> GetCountNotifications(NotificationFilter filter, CancellationToken ct);
    Task<List<NotificationItem>> GetPagedNotifications(NotificationFilter filter, CancellationToken ct);
}