using CRMSystem.Core.ProjectionModels.NotificationType;

namespace CRMSystem.Core.Abstractions;

public interface INotificationTypeRepository
{
    Task<List<NotificationTypeItem>> Get(CancellationToken ct);
    Task<NotificationTypeItem?> GetById(int id, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}