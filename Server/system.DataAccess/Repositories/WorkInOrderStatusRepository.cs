using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkInOrderStatusRepository : IWorkInOrderStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public WorkInOrderStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkInOrderStatusItem>> Get()
    {
        return await _context.WorkInOrderStatuses
            .AsNoTracking()
            .ProjectTo<WorkInOrderStatusItem>(_mapper.ConfigurationProvider )
            .ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.WorkInOrderStatuses
            .AsNoTracking()
            .AnyAsync(w => w.Id == id);
    }
}
