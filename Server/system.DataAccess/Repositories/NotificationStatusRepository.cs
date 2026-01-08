using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationStatusRepository : INotificationStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public NotificationStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<NotificationStatusItem>> Get(CancellationToken ct)
    {
        return await _context.NotificationsStatuses
            .AsNoTracking()
            .ProjectTo<NotificationStatusItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.NotificationsStatuses
            .AsNoTracking()
            .AnyAsync(n => n.Id == id, ct);
    }
}
