// Ignore Spelling: repo

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class NotificationStatusService(
    INotificationStatusRepository repo,
    ILogger<NotificationStatusService> logger) : INotificationStatusService
{
    public async Task<List<NotificationStatusItem>> GetNotificationStatuses(CancellationToken ct)
    {
        logger.LogInformation("Notification status getting start");

        var carSatsus = await repo.Get(ct);

        logger.LogInformation("Notification status getting success");

        return carSatsus;
    }
}
