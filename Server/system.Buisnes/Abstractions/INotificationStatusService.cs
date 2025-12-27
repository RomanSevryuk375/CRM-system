using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface INotificationStatusService
{
    Task<List<NotificationStatusItem>> GetNotificationStatuses();
}