using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class BillStatusRepository(
    SystemDbContext context,
    IMapper mapper) : IBillStatusRepository
{
    public async Task<List<BillStatusItem>> Get(CancellationToken ct)
    {
        return await context.BillStatuses
            .AsNoTracking()
            .ProjectTo<BillStatusItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists (int id, CancellationToken ct)
    {
        return await context.BillStatuses
            .AsNoTracking()
            .AnyAsync(b => b.Id == id, ct);
    }
}
