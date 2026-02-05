using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationStatusRepository(
    SystemDbContext context,
    IMapper mapper) : INotificationStatusRepository
{
    public async Task<List<NotificationStatusItem>> Get(CancellationToken ct)
    {
        return await context.NotificationsStatuses
            .AsNoTracking()
            .ProjectTo<NotificationStatusItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.NotificationsStatuses
            .AsNoTracking()
            .AnyAsync(n => n.Id == id, ct);
    }
}
