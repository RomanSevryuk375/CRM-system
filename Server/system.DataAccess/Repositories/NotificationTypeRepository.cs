using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationTypeRepository : INotificationTypeRepository
{
    private readonly SystemDbContext _context;

    public NotificationTypeRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<NotificationTypeItem>> Get()
    {
        var query = _context.NotificationTypes.AsNoTracking();

        var projection = query.Select(n => new NotificationTypeItem(
            n.Id,
            n.Name));

        return await projection.ToListAsync();
    }
}
