using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.DTOs;
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

    public async Task<List<WorkProposalStatusItem>> Get()
    {
        return await _context.WorkProposalStatuses
            .AsNoTracking()
            .ProjectTo<WorkProposalStatusItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.WorkProposalStatuses
            .AsNoTracking()
            .AnyAsync(w => w.Id == id);
    }
}
