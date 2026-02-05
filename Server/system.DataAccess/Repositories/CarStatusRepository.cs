using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class CarStatusRepository(
    SystemDbContext context,
    IMapper mapper) : ICarStatusRepository
{
    public async Task<List<CarStatusItem>> Get(CancellationToken ct)
    {
        return await context.CarStatuses
            .AsNoTracking()
            .ProjectTo<CarStatusItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await context.CarStatuses
            .AnyAsync(c => c.Id == id, ct);
    }
}
