using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface INotificationTypeRepository
{
    Task<List<NotificationTypeItem>> Get();
    Task<bool> Exists(int id);
}