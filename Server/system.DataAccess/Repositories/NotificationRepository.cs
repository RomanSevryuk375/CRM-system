using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationRepository(
    SystemDbContext context,
    IMapper mapper) : INotificationRepository
{
    private static IQueryable<NotificationEntity> ApplyFilter (
        IQueryable<NotificationEntity> query, NotificationFilter filter)
    {
        if (filter.ClientIds != null && filter.ClientIds.Any())
        {
            query = query.Where(n => filter.ClientIds.Contains(n.ClientId));
        }

        if (filter.CarIds != null && filter.CarIds.Any())
        {
            query = query.Where(n => filter.CarIds.Contains(n.CarId));
        }

        if (filter.TypeIds != null && filter.TypeIds.Any())
        {
            query = query.Where(n => filter.TypeIds.Contains(n.TypeId));
        }

        if (filter.StatusIds != null && filter.StatusIds.Any())
        {
            query = query.Where(n => filter.StatusIds.Contains(n.StatusId));
        }

        return query;
    }

    public async Task<List<NotificationItem>> GetPaged(NotificationFilter filter, CancellationToken ct)
    {
        var query = context.Notifications.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "client" => filter.IsDescending
                ? query.OrderByDescending(n => n.Client == null
                    ? string.Empty
                    : n.Client.Surname)
                : query.OrderBy(n => n.Client == null
                    ? string.Empty
                    : n.Client.Surname),

            "car" => filter.IsDescending
                ? query.OrderByDescending(n => n.Car == null
                    ? string.Empty
                    : n.Car.Brand)
                : query.OrderBy(n => n.Car == null
                    ? string.Empty
                    : n.Car.Brand),

            "type" => filter.IsDescending
                ? query.OrderByDescending(n => n.NotificationType == null
                    ? string.Empty
                    : n.NotificationType.Name)
                : query.OrderBy(n => n.NotificationType == null
                    ? string.Empty
                    : n.NotificationType.Name),

            "status" => filter.IsDescending
                ? query.OrderByDescending(n => n.Status == null
                    ? string.Empty
                    : n.Status.Name)
                : query.OrderBy(n => n.Status == null
                    ? string.Empty
                    : n.Status.Name),

            "message" => filter.IsDescending
                ? query.OrderByDescending(n => n.Message)
                : query.OrderBy(n => n.Message),

            "sendat" => filter.IsDescending
                ? query.OrderByDescending(n => n.SendAt)
                : query.OrderBy(n => n.SendAt),

            _ => filter.IsDescending
                ? query.OrderByDescending(n => n.Id)
                : query.OrderBy(n => n.Id),
        };

        return await query
            .ProjectTo<NotificationItem>(mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(NotificationFilter filter, CancellationToken ct)
    {
        var query = context.Notifications.AsNoTracking();

        query = ApplyFilter(query, filter);

        return await query.CountAsync(ct);
    }

    public async Task<long> Create(Notification notification, CancellationToken ct)
    {
        var notificationEntity = new NotificationEntity
        {
            ClientId = notification.ClientId,
            CarId = notification.CarId,
            TypeId = (int)notification.TypeId,
            StatusId = (int)notification.StatusId,
            Message = notification.Message,
            SendAt = notification.SendAt,
        };

        await context.AddAsync(notificationEntity, ct);
        await context.SaveChangesAsync(ct);

        return notificationEntity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        await context.Notifications
            .Where(n => n.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
