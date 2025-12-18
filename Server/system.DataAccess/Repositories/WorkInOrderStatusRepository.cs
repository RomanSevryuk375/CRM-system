using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkInOrderStatusRepository : IWorkInOrderStatusRepository
{
    private readonly SystemDbContext _context;

    public WorkInOrderStatusRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkInOrderStatusItem>> Get()
    {
        var query = _context.WorkInOrderStatuses.AsNoTracking();

        var projection = query.Select(w => new WorkInOrderStatusItem(
            w.Id,
            w.Name));

        return await projection.ToListAsync();
    }
}
