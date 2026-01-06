using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkProposalRepository : IWorkProposalRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public WorkProposalRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private IQueryable<WorkProposalEntity> ApplyFilter(IQueryable<WorkProposalEntity> query, WorkProposalFilter filter)
    {
        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(w => filter.OrderIds.Contains(w.OrderId));

        if (filter.JobIds != null && filter.JobIds.Any())
            query = query.Where(w => filter.JobIds.Contains(w.JobId));

        if (filter.WorkerIds != null && filter.WorkerIds.Any())
            query = query.Where(w => filter.WorkerIds.Contains(w.WorkerId));

        if (filter.StatusIds != null && filter.StatusIds.Any())
            query = query.Where(w => filter.StatusIds.Contains(w.StatusId));

        return query;
    }

    public async Task<List<WorkProposalItem>> GetPaged(WorkProposalFilter filter)
    {
        var query = _context.WorkProposals.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "order" => filter.IsDescending
                ? query.OrderByDescending(w => w.OrderId)
                : query.OrderBy(w => w.OrderId),
            "job" => filter.IsDescending
                ? query.OrderByDescending(w => w.Work == null
                    ? string.Empty
                    : w.Work.Title)
                : query.OrderBy(w => w.Work == null
                    ? string.Empty
                    : w.Work.Title),
            "worker" => filter.IsDescending
                ? query.OrderByDescending(w => w.Worker == null
                    ? string.Empty
                    : w.Worker.Surname)
                : query.OrderBy(w => w.Worker == null
                    ? string.Empty
                    : w.Worker.Surname),
            "status" => filter.IsDescending
                ? query.OrderByDescending(w => w.Status == null
                    ? string.Empty
                    : w.Status.Name)
                : query.OrderBy(w => w.Status == null
                    ? string.Empty
                    : w.Status.Name),
            "date" => filter.IsDescending
                ? query.OrderByDescending(w => w.Date)
                : query.OrderBy(w => w.Date),

            _ => filter.IsDescending
                ? query.OrderByDescending(w => w.Id)
                : query.OrderBy(w => w.Id),
        };

        return await query
            .ProjectTo<WorkProposalItem>(_mapper.ConfigurationProvider)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<WorkProposalItem?> GetById(long id)
    {
        return await _context.WorkProposals
            .AsNoTracking()
            .Where(p => p.Id == id)
            .ProjectTo<WorkProposalItem>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetCount(WorkProposalFilter filter)
    {
        var query = _context.WorkProposals.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(WorkProposal workProposal)
    {
        var workProposalEntity = new WorkProposalEntity
        {
            OrderId = workProposal.OrderId,
            JobId = workProposal.JobId,
            WorkerId = workProposal.WorkerId,
            StatusId = (int)workProposal.StatusId,
            Date = workProposal.Date
        };

        await _context.WorkProposals.AddAsync(workProposalEntity);
        await _context.SaveChangesAsync();

        return workProposalEntity.Id;
    }

    public async Task<long> Update(long id, ProposalStatusEnum? statusId)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Work proposal not found");

        if (statusId.HasValue) workProposal.StatusId = (int)statusId.Value;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<long> AcceptProposal(long id)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id)
            ?? throw new Exception("Work proposal not found");

        workProposal.StatusId = (int)ProposalStatusEnum.Accepted;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<long> RejectProposal(long id)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id)
            ?? throw new Exception("Work proposal not found");

        workProposal.StatusId = (int)ProposalStatusEnum.Rejected;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<long> Delete(long id)
    {
        var workProposal = await _context.WorkProposals
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.WorkProposals
            .AsNoTracking()
            .AnyAsync(x => x.Id == id);
    }
}
