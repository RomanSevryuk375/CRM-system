using CRMSystem.Core.DTOs.Notification;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly SystemDbContext _context;

    public NotificationRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<NotificationEntity> ApplyFilter
        (IQueryable<NotificationEntity> query, NotificationFilter filter)
    {
        if (filter.clientIds != null && filter.clientIds.Any())
            query = query.Where(n => filter.clientIds.Contains(n.ClientId));

        if (filter.carIds != null && filter.carIds.Any())
            query = query.Where(n => filter.carIds.Contains(n.CarId));

        if (filter.typeIds != null && filter.typeIds.Any())
            query = query.Where(n => filter.typeIds.Contains(n.TypeId));

        if (filter.statusIds != null && filter.statusIds.Any())
            query = query.Where(n => filter.statusIds.Contains(n.StatusId));

        return query;
    }

    public async Task<List<NotificationItem>> GetPaged(NotificationFilter filter)
    {
        var query = _context.Notifications.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "client" => filter.isDescending
                ? query.OrderByDescending(n => n.Client == null
                    ? string.Empty
                    : n.Client.Surname)
                : query.OrderBy(n => n.Client == null
                    ? string.Empty
                    : n.Client.Surname),
            "car" => filter.isDescending
                ? query.OrderByDescending(n => n.Car == null
                    ? string.Empty
                    : n.Car.Brand)
                : query.OrderBy(n => n.Car == null
                    ? string.Empty
                    : n.Car.Brand),
            "type" => filter.isDescending
                ? query.OrderByDescending(n => n.NotificationType == null
                    ? string.Empty
                    : n.NotificationType.Name)
                : query.OrderBy(n => n.NotificationType == null
                    ? string.Empty
                    : n.NotificationType.Name),
            "status" => filter.isDescending
                ? query.OrderByDescending(n => n.Status == null
                    ? string.Empty
                    : n.Status.Name)
                : query.OrderBy(n => n.Status == null
                    ? string.Empty
                    : n.Status.Name),
            "message" => filter.isDescending
                ? query.OrderByDescending(n => n.Message)
                : query.OrderBy(n => n.Message),
            "sendat" => filter.isDescending
                ? query.OrderByDescending(n => n.SendAt)
                : query.OrderBy(n => n.SendAt),

            _ => filter.isDescending
                ? query.OrderByDescending(n => n.Id)
                : query.OrderBy(n => n.Id),
        };

        var projection = query.Select(n => new NotificationItem(
            n.Id,
            n.Client == null
                ? string.Empty
                : $"{n.Client.Name} {n.Client.Surname}",
            n.Car == null
                ? string.Empty
                : $"{n.Car.Brand} ({n.Car.StateNumber})",
            n.NotificationType == null
                ? string.Empty
                : n.NotificationType.Name,
            n.Status == null
                ? string.Empty
                : n.Status.Name,
            n.Message,
            n.SendAt));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<int> GetCount(NotificationFilter filter)
    {
        var query = _context.Notifications.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Notification notification)
    {
        var noticationEntity = new NotificationEntity
        {
            ClientId = notification.ClientId,
            CarId = notification.CarId,
            TypeId = notification.TypeId,
            StatusId = notification.StatusId,
            Message = notification.Message,
            SendAt = notification.SendAt,
        };

        await _context.AddAsync(noticationEntity);
        await _context.SaveChangesAsync();

        return noticationEntity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var entity = await _context.Notifications
            .Where(n => n.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
