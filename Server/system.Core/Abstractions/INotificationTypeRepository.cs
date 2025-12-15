using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface INotificationTypeRepository
    {
        Task<List<NotificationTypeItem>> Get();
    }
}