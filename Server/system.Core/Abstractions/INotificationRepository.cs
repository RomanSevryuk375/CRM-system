using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface INotificationRepository
{
    Task<long> Create(Notification notification);
    Task<long> Delete(long id);
    Task<int> GetCount(NotificationFilter filter);
    Task<List<NotificationItem>> GetPaged(NotificationFilter filter);
}