// Ignore Spelling: repo

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.NotificationStatus;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class NotificationStatusService(
    INotificationStatusRepository repo,
    ILogger<NotificationStatusService> logger) : INotificationStatusService
{
    public async Task<NotificationStatusItem> GetNotificationStatusById(int id, CancellationToken ct)
    {
        return await repo.GetById(id, ct)
               ?? throw new NotFoundException($"NotificationStatus {id} not found");
    }
    public async Task<List<NotificationStatusItem>> GetNotificationStatuses(CancellationToken ct)
    {
        logger.LogInformation("Notification status getting start");

        var carStatus = await repo.Get(ct);

        logger.LogInformation("Notification status getting success");

        return carStatus;
    }
}
