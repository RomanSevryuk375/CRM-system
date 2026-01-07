using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IClientRepository _clientRepository;
    private readonly ICarRepository _carRepository;
    private readonly INotificationStatusRepository _notificationStatusRepository;
    private readonly INotificationTypeRepository _notificationTypeRepository;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        INotificationRepository notificationRepository,
        IClientRepository clientRepository,
        ICarRepository carRepository,
        INotificationStatusRepository notificationStatusRepository,
        INotificationTypeRepository notificationTypeRepository,
        ILogger<NotificationService> logger)
    {
        _notificationRepository = notificationRepository;
        _clientRepository = clientRepository;
        _carRepository = carRepository;
        _notificationStatusRepository = notificationStatusRepository;
        _notificationTypeRepository = notificationTypeRepository;
        _logger = logger;
    }

    public async Task<List<NotificationItem>> GetPagedNotifications(NotificationFilter filter)
    {
        _logger.LogInformation("Getting notifications start");

        var notifications = await _notificationRepository.GetPaged(filter);

        _logger.LogInformation("Getting notifications success");

        return notifications;
    }

    public async Task<int> GetCountNotifications(NotificationFilter filter)
    {
        _logger.LogInformation("Getting notifications count start");

        var count = await _notificationRepository.GetCount(filter);

        _logger.LogInformation("Getting notifications count success");

        return count;
    }

    public async Task<long> CreateNotification(Notification notification)
    {
        _logger.LogInformation("Creating notification start");

        if (!await _clientRepository.Exists(notification.ClientId))
        {
            _logger.LogError("Client {ClientId} not found", notification.ClientId);
            throw new NotFoundException($"Client {notification.ClientId} not found");
        }

        if (!await _carRepository.Exists(notification.CarId))
        {
            _logger.LogError("Car {CarId} not found", notification.CarId);
            throw new NotFoundException($"Car {notification.CarId} not found");
        }

        if (!await _notificationStatusRepository.Exists((int)notification.StatusId))
        {
            _logger.LogError("Status {StatusId} not found", (int)notification.StatusId);
            throw new NotFoundException($"Status {(int)notification.StatusId} not found");
        }

        if (!await _notificationTypeRepository.Exists((int)notification.TypeId))
        {
            _logger.LogError("Type {TypeId} not found", (int)notification.TypeId);
            throw new NotFoundException($"Type {(int)notification.TypeId} not found");
        }

        var id = await _notificationRepository.Create(notification);

        _logger.LogInformation("Creating notification success");

        return id;
    }

    public async Task<long> DeleteNotification(long id)
    {
        _logger.LogInformation("Deleting notification start");

        var deletedId = await _notificationRepository.Delete(id);

        _logger.LogInformation("Deleting notification success");

        return deletedId;
    }
}
