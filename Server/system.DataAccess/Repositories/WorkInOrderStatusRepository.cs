using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkInOrderStatusRepository(
    SystemDbContext context,
    IMapper mapper) : IWorkInOrderStatusRepository
{
    public async Task<List<WorkInOrderStatusItem>> Get(CancellationToken ct)
    {
        return await context.WorkInOrderStatuses
            .AsNoTracking()
            .ProjectTo<WorkInOrderStatusItem>(mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.WorkInOrderStatuses
            .AsNoTracking()
            .AnyAsync(w => w.Id == id, ct);
    }
}
