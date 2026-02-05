using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkProposalStatusRepository(
    SystemDbContext context,
    IMapper mapper) : IWorkProposalStatusRepository
{
    public async Task<List<WorkProposalStatusItem>> Get(CancellationToken ct)
    {
        return await context.WorkProposalStatuses
            .AsNoTracking()
            .ProjectTo<WorkProposalStatusItem>(mapper.ConfigurationProvider,ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await context.WorkProposalStatuses
            .AsNoTracking()
            .AnyAsync(w => w.Id == id, ct);
    }
}
