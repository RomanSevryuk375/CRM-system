using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class BillStatusRepository : IBillStatusRepository
{
    private readonly SystemDbContext _context;

    public BillStatusRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<BillStatusItem>> Get()
    {
        var query = _context.BillStatuses.AsNoTracking();

        var progection = query.Select(b => new BillStatusItem(
            b.Id,
            b.Name));

        return await progection.ToListAsync();
    }

    public async Task<bool> Exists (int id)
    {
        return await _context.BillStatuses
            .AsNoTracking()
            .AnyAsync(b => b.Id == id);
    }
}
