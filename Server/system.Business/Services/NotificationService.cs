using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class NotificationService(
    INotificationRepository notificationRepository,
    IClientRepository clientRepository,
    ICarRepository carRepository,
    INotificationStatusRepository notificationStatusRepository,
    INotificationTypeRepository notificationTypeRepository,
    ILogger<NotificationService> logger) : INotificationService
{
    public async Task<List<NotificationItem>> GetPagedNotifications(NotificationFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting notifications start");

        var notifications = await notificationRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting notifications success");

        return notifications;
    }

    public async Task<int> GetCountNotifications(NotificationFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting notifications count start");

        var count = await notificationRepository.GetCount(filter, ct);

        logger.LogInformation("Getting notifications count success");

        return count;
    }

    public async Task<long> CreateNotification(Notification notification, CancellationToken ct)
    {
        logger.LogInformation("Creating notification start");

        if (!await clientRepository.Exists(notification.ClientId, ct))
        {
            logger.LogError("Client {ClientId} not found", notification.ClientId);
            throw new NotFoundException($"Client {notification.ClientId} not found");
        }

        if (!await carRepository.Exists(notification.CarId, ct))
        {
            logger.LogError("Car {CarId} not found", notification.CarId);
            throw new NotFoundException($"Car {notification.CarId} not found");
        }

        if (!await notificationStatusRepository.Exists((int)notification.StatusId, ct))
        {
            logger.LogError("Status {StatusId} not found", (int)notification.StatusId);
            throw new NotFoundException($"Status {(int)notification.StatusId} not found");
        }

        if (!await notificationTypeRepository.Exists((int)notification.TypeId, ct))
        {
            logger.LogError("Type {TypeId} not found", (int)notification.TypeId);
            throw new NotFoundException($"Type {(int)notification.TypeId} not found");
        }

        var id = await notificationRepository.Create(notification, ct);

        logger.LogInformation("Creating notification success");

        return id;
    }

    public async Task<long> DeleteNotification(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting notification start");

        var deletedId = await notificationRepository.Delete(id, ct);

        logger.LogInformation("Deleting notification success");

        return deletedId;
    }
}
