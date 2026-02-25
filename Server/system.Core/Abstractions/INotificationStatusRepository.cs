using CRMSystem.Core.ProjectionModels.NotificationStatus;

namespace CRMSystem.Core.Abstractions;

public interface INotificationStatusRepository
{
    Task<List<NotificationStatusItem>> Get(CancellationToken ct);
    Task<NotificationStatusItem?> GetById(int id, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}