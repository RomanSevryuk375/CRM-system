using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface INotificationTypeRepository
{
    Task<List<NotificationTypeItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}