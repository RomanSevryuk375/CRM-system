using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderStatusRepository : IOrderStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public OrderStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OrderStatusItem>> Get(CancellationToken ct)
    {
        return await _context.OrderStatuses
            .AsNoTracking()
            .ProjectTo<OrderStatusItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.OrderStatuses
            .AsNoTracking()
            .AnyAsync(o => o.Id == id, ct);
    }
}
