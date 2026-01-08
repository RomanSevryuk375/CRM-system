// Ignore Spelling: repo

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class NotificationStatusService : INotificationStatusService
{
    private readonly INotificationStatusRepository _repo;
    private readonly ILogger<NotificationStatusService> _logger;

    public NotificationStatusService(
        INotificationStatusRepository repo,
        ILogger<NotificationStatusService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<List<NotificationStatusItem>> GetNotificationStatuses(CancellationToken ct)
    {
        _logger.LogInformation("Notification status getting start");

        var carSatsus = await _repo.Get(ct);

        _logger.LogInformation("Notification status getting success");

        return carSatsus;
    }
}
