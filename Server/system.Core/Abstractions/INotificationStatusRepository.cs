using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface INotificationStatusRepository
{
    Task<List<NotificationStatusItem>> Get();
    Task<bool> Exists(int id);
}