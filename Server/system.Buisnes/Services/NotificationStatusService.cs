using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<NotificationStatusItem>> GetNotificationStatuses()
    {
        _logger.LogInformation("Notification status getting start");

        var carSatsus = await _repo.Get();

        _logger.LogInformation("Notification status getting success");

        return carSatsus;
    }
}
