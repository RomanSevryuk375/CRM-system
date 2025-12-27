using CRMSystem.Core.DTOs.Notification;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface INotificationService
{
    Task<long> CreateNotification(Notification notification);
    Task<long> DeleteNotification(long id);
    Task<int> GetCountNotifications(NotificationFilter filter);
    Task<List<NotificationItem>> GetPagedNotifications(NotificationFilter filter);
}