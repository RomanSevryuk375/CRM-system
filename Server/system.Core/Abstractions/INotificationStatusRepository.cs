using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface INotificationStatusRepository
{
    Task<List<NotificationStatusItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}