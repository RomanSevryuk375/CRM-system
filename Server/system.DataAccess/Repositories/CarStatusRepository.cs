using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class CarStatusRepository : ICarStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public CarStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CarStatusItem>> Get(CancellationToken ct)
    {
        return await _context.CarStatuses
            .AsNoTracking()
            .ProjectTo<CarStatusItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await _context.CarStatuses
            .AnyAsync(c => c.Id == id, ct);
    }
}
