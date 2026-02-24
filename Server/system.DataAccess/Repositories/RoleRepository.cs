using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Role;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class RoleRepository(
    SystemDbContext context,
    IMapper mapper) : IRoleRepository
{
    public async Task<RoleItem?> GetById(int id, CancellationToken ct)
    {
        return await context.Roles
            .AsNoTracking()
            .Where(r => r.Id == id)
            .ProjectTo<RoleItem>(mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }
    
    public async Task<List<RoleItem>> Get(CancellationToken ct)
    {
        return await context.Roles
            .AsNoTracking()
            .ProjectTo<RoleItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }
}
