using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface INotificationStatusService
{
    Task<List<NotificationStatusItem>> GetNotificationStatuses();
}