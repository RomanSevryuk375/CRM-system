using CRMSystem.Core.DTOs.Notification;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface INotificationRepository
    {
        Task<long> Create(Notification notification);
        Task<long> Delete(long id);
        Task<int> GetCount(NotificationFilter filter);
        Task<List<NotificationItem>> GetPaged(NotificationFilter filter);
    }
}