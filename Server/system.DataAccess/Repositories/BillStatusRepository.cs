using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class BillStatusRepository : IBillStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public BillStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<BillStatusItem>> Get()
    {
        return await _context.BillStatuses
            .AsNoTracking()
            .ProjectTo<BillStatusItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists (int id)
    {
        return await _context.BillStatuses
            .AsNoTracking()
            .AnyAsync(b => b.Id == id);
    }
}
