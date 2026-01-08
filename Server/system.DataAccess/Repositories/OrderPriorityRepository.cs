using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderPriorityRepository : IOrderPriorityRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public OrderPriorityRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderPriorityItem>> Get(CancellationToken ct)
    {
        return await _context.OrderPriorities
            .AsNoTracking()
            .ProjectTo<OrderPriorityItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists (int id, CancellationToken ct)
    {
        return await _context.OrderPriorities
            .AsNoTracking()
            .AnyAsync(o => o.Id == id, ct);
    }
}
