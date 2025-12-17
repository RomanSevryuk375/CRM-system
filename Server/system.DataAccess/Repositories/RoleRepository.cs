using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly SystemDbContext _context;

    public RoleRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoleItem>> Get()
    {
        var query = _context.Roles.AsNoTracking();

        var projection = query.Select(r => new RoleItem(
            r.Id,
            r.Name));

        return await projection.ToListAsync();
    }
}
