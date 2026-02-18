using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.NotificationStatus;

namespace CRMSystem.Business.Abstractions;

public interface INotificationStatusService
{
    Task<List<NotificationStatusItem>> GetNotificationStatuses(CancellationToken ct);
}