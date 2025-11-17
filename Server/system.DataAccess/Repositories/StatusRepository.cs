using CRMSystem.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class StatusRepository : IStatusRepository
{
    private readonly SystemDbContext _context;

    public StatusRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<Status>> Get()
    {
        var statusEntities = await _context.Statuses
            .AsNoTracking()
            .ToListAsync();

        var status = statusEntities
            .Select(s => Status.Create(s.Id, s.Name, s.Description).status)
            .ToList();

        return status;
    }

}
