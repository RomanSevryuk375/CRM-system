using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class CarStatusRepository
{
    private readonly SystemDbContext _context;

    public CarStatusRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<CarStatusItem>> Get ()
    {
        var query = _context.CarStatuses.AsNoTracking();

        var projection = query.Select(c => new CarStatusItem(
            c.Id,
            c.Name));

        return await projection.ToListAsync();
    }
}
