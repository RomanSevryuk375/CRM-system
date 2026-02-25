// Ignore Spelling: repo

using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.NotificationType;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class NotificationTypeService(
    INotificationTypeRepository repo,
    ILogger<NotificationTypeService> logger) : INotificationTypeService
{
    public async Task<NotificationTypeItem> GetNotificationTypeById(int id, CancellationToken ct)
    {
        return await repo.GetById(id, ct)
               ?? throw new NotFoundException($"NotificationType {id} not found");
    }
    
    public async Task<List<NotificationTypeItem>> GetNotificationTypes(CancellationToken ct)
    {
        logger.LogInformation("Getting NotificationType start");

        var absenceType = await repo.Get(ct);

        logger.LogInformation("Getting NotificationType success");

        return absenceType;
    }
}
