using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationStatusRepository : INotificationStatusRepository
{
    private readonly SystemDbContext _context;

    public NotificationStatusRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationStatusItem>> Get()
    {
        var query = _context.NotificationsStatuses.AsNoTracking();

        var projection = query.Select(n => new NotificationStatusItem(
            n.Id,
            n.Name));

        return await projection.ToListAsync();
    }
}
