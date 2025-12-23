using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Services
{
    public interface INotificationTypeService
    {
        Task<List<NotificationTypeItem>> GetNotificationTypes();
    }
}