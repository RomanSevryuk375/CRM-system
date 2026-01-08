// Ignore Spelling: repo

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

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

    public async Task<List<NotificationTypeItem>> GetNotificationTypes(CancellationToken ct)
    {
        _logger.LogInformation("Getting NotificationType start");

        var absenceType = await _repo.Get(ct);

        _logger.LogInformation("Getting NotificationType success");

        return absenceType;
    }
}
