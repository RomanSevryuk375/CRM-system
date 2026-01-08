using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public RoleRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RoleItem>> Get(CancellationToken ct)
    {
        return await _context.Roles
            .AsNoTracking()
            .ProjectTo<RoleItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }
}
