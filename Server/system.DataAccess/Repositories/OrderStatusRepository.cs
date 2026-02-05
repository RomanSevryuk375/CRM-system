using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderStatusRepository(
    SystemDbContext context,
    IMapper mapper) : IOrderStatusRepository
{
    public async Task<List<OrderStatusItem>> Get(CancellationToken ct)
    {
        return await context.OrderStatuses
            .AsNoTracking()
            .ProjectTo<OrderStatusItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.OrderStatuses
            .AsNoTracking()
            .AnyAsync(o => o.Id == id, ct);
    }
}
