using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
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

    public async Task<List<NotificationStatusItem>> Get()
    {
        return await _context.NotificationsStatuses
            .AsNoTracking()
            .ProjectTo<NotificationStatusItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.NotificationsStatuses
            .AsNoTracking()
            .AnyAsync(n => n.Id == id);
    }
}
