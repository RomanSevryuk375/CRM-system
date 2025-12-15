using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface INotificationStatusRepository
    {
        Task<List<NotificationStatusItem>> Get();
    }
}