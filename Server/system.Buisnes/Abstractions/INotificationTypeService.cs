using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface INotificationTypeService
{
    Task<List<NotificationTypeItem>> GetNotificationTypes();
}