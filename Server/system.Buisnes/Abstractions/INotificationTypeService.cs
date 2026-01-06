using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface INotificationTypeService
{
    Task<List<NotificationTypeItem>> GetNotificationTypes();
}