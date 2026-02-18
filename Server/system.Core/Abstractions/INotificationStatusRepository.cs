using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.NotificationStatus;

namespace CRMSystem.Core.Abstractions;

public interface INotificationStatusRepository
{
    Task<List<NotificationStatusItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}