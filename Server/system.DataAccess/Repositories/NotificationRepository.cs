using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs.Notification;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public NotificationRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private IQueryable<NotificationEntity> ApplyFilter
        (IQueryable<NotificationEntity> query, NotificationFilter filter)
    {
        if (filter.ClientIds != null && filter.ClientIds.Any())
            query = query.Where(n => filter.ClientIds.Contains(n.ClientId));

        if (filter.CarIds != null && filter.CarIds.Any())
            query = query.Where(n => filter.CarIds.Contains(n.CarId));

        if (filter.TypeIds != null && filter.TypeIds.Any())
            query = query.Where(n => filter.TypeIds.Contains(n.TypeId));

        if (filter.StatusIds != null && filter.StatusIds.Any())
            query = query.Where(n => filter.StatusIds.Contains(n.StatusId));

        return query;
    }

    public async Task<List<NotificationItem>> GetPaged(NotificationFilter filter)
    {
        var query = _context.Notifications.AsNoTracking();
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
            .ProjectTo<NotificationItem>(_mapper.ConfigurationProvider)
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
            TypeId = (int)notification.TypeId,
            StatusId = (int)notification.StatusId,
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
