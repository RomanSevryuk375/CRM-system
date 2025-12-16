using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderPriorityRepository : IOrderPriorityRepository
{
    private readonly SystemDbContext _context;

    public OrderPriorityRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderPriorityItem>> Get()
    {
        var query = _context.OrderPriorities.AsNoTracking();

        var projection = query.Select(o => new OrderPriorityItem(
            o.Id,
            o.Name));

        return await projection.ToListAsync();
    }
}
