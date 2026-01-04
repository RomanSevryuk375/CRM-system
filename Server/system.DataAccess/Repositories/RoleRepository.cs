using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
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

    public async Task<List<RoleItem>> Get()
    {
        return await _context.Roles
            .AsNoTracking()
            .ProjectTo<RoleItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
