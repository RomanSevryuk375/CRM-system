using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderStatusRepository : IOrderStatusRepository
{
    private readonly SystemDbContext _context;

    public OrderStatusRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderStatusItem>> Get()
    {
        var query = _context.OrderStatuses.AsNoTracking();

        var projection = query.Select(o => new OrderStatusItem(
            o.Id,
            o.Name));

        return await projection.ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.OrderStatuses
            .AsNoTracking()
            .AnyAsync(o => o.Id == id);
    }
}
