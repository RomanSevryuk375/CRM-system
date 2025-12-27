using CRMSystem.Core.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkProposalStatusRepository : IWorkProposalStatusRepository
{
    private readonly SystemDbContext _context;

    public WorkProposalStatusRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkProposalStatusItem>> Get()
    {
        var query = _context.WorkProposalStatuses.AsNoTracking();

        var projection = query.Select(w => new WorkProposalStatusItem(
            w.Id,
            w.Name));

        return await projection.ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.WorkProposalStatuses
            .AsNoTracking()
            .AnyAsync(w => w.Id == id);
    }
}
