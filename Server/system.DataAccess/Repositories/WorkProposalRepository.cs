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

    private static IQueryable<WorkProposalEntity> ApplyFilter(IQueryable<WorkProposalEntity> query, WorkProposalFilter filter)
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

    public async Task<List<WorkProposalItem>> GetPaged(WorkProposalFilter filter, CancellationToken ct)
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
            .ProjectTo<WorkProposalItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<WorkProposalItem?> GetById(long id, CancellationToken ct)
    {
        return await _context.WorkProposals
            .AsNoTracking()
            .Where(p => p.Id == id)
            .ProjectTo<WorkProposalItem>(_mapper.ConfigurationProvider, ct)
            .FirstOrDefaultAsync(ct);
    }

    public async Task<int> GetCount(WorkProposalFilter filter, CancellationToken ct)
    {
        var query = _context.WorkProposals.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<long> Create(WorkProposal workProposal, CancellationToken ct)
    {
        var workProposalEntity = new WorkProposalEntity
        {
            OrderId = workProposal.OrderId,
            JobId = workProposal.JobId,
            WorkerId = workProposal.WorkerId,
            StatusId = (int)workProposal.StatusId,
            Date = workProposal.Date
        };

        await _context.WorkProposals.AddAsync(workProposalEntity, ct);
        await _context.SaveChangesAsync(ct);

        return workProposalEntity.Id;
    }

    public async Task<long> Update(long id, ProposalStatusEnum? statusId, CancellationToken ct)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new Exception("Work proposal not found");

        if (statusId.HasValue) workProposal.StatusId = (int)statusId.Value;

        await _context.SaveChangesAsync(ct);

        return workProposal.Id;
    }

    public async Task<long> AcceptProposal(long id, CancellationToken ct)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id, ct)
            ?? throw new Exception("Work proposal not found");

        workProposal.StatusId = (int)ProposalStatusEnum.Accepted;

        await _context.SaveChangesAsync(ct);

        return workProposal.Id;
    }

    public async Task<long> RejectProposal(long id, CancellationToken ct)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id, ct)
            ?? throw new Exception("Work proposal not found");

        workProposal.StatusId = (int)ProposalStatusEnum.Rejected;

        await _context.SaveChangesAsync(ct);

        return workProposal.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var workProposal = await _context.WorkProposals
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }

    public async Task<bool> Exists(long id, CancellationToken ct)
    {
        return await _context.WorkProposals
            .AsNoTracking()
            .AnyAsync(x => x.Id == id, ct);
    }
}
