using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class NotificationTypeRepository : INotificationTypeRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public NotificationTypeRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.NotificationTypes
            .AsNoTracking()
            .AnyAsync(n => n.Id == id);
    }

    public async Task<List<NotificationTypeItem>> Get()
    {
        return await _context.NotificationTypes
            .AsNoTracking()
            .ProjectTo<NotificationTypeItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
