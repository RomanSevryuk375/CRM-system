using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface INotificationRepository
{
    Task<long> Create(Notification notification, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(NotificationFilter filter, CancellationToken ct);
    Task<List<NotificationItem>> GetPaged(NotificationFilter filter, CancellationToken ct);
}