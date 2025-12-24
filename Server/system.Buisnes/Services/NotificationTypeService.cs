using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class NotificationTypeService : INotificationTypeService
{
    private readonly INotificationTypeRepository _repo;
    private readonly ILogger<NotificationTypeService> _logger;

    public NotificationTypeService(
        INotificationTypeRepository repo,
        ILogger<NotificationTypeService> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    public async Task<List<NotificationTypeItem>> GetNotificationTypes()
    {
        _logger.LogInformation("Getting NotificationType start");

        var absenceType = await _repo.Get();

        _logger.LogInformation("Getting NotificationType success");

        return absenceType;
    }
}
