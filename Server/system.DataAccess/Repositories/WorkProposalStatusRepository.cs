using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkProposalStatusRepository : IWorkProposalStatusRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public WorkProposalStatusRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkProposalStatusItem>> Get(CancellationToken ct)
    {
        return await _context.WorkProposalStatuses
            .AsNoTracking()
            .ProjectTo<WorkProposalStatusItem>(_mapper.ConfigurationProvider,ct)
            .ToListAsync(ct);
    }

    public async Task<bool> Exists(int id, CancellationToken ct)
    {
        return await _context.WorkProposalStatuses
            .AsNoTracking()
            .AnyAsync(w => w.Id == id, ct);
    }
}
