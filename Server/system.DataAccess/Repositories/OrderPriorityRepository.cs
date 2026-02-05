using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderPriorityRepository(
    SystemDbContext context,
    IMapper mapper) : IOrderPriorityRepository
{
    public async Task<List<OrderPriorityItem>> Get(CancellationToken ct)
    {
        return await context.OrderPriorities
            .AsNoTracking()
            .ProjectTo<OrderPriorityItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists (int id, CancellationToken ct)
    {
        return await context.OrderPriorities
            .AsNoTracking()
            .AnyAsync(o => o.Id == id, ct);
    }
}
