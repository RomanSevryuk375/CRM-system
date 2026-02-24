using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.NotificationType;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationTypeRepository(
    SystemDbContext context,
    IMapper mapper) : INotificationTypeRepository
{
    public async Task<NotificationTypeItem?> GetById(int id, CancellationToken ct)
    {
        return await context.NotificationTypes
            .AsNoTracking()
            .Where(a => a.Id == id)
            .ProjectTo<NotificationTypeItem>(mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }
    
    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.NotificationTypes
            .AsNoTracking()
            .AnyAsync(n => n.Id == id, ct);
    }

    public async Task<List<NotificationTypeItem>> Get(CancellationToken ct)
    {
        return await context.NotificationTypes
            .AsNoTracking()
            .ProjectTo<NotificationTypeItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }
}
