using CRMSystem.Core.DTOs.WorkProposal;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkProposalRepository : IWorkProposalRepository
{
    private readonly SystemDbContext _context;

    public WorkProposalRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<WorkProposalEntity> ApplyFilter(IQueryable<WorkProposalEntity> query, WorkProposalFilter filter)
    {
        if (filter.orderIds != null && filter.orderIds.Any())
            query = query.Where(w => filter.orderIds.Contains(w.OrderId));

        if (filter.jobIds != null && filter.jobIds.Any())
            query = query.Where(w => filter.jobIds.Contains(w.JobId));

        if (filter.workerIds != null && filter.workerIds.Any())
            query = query.Where(w => filter.workerIds.Contains(w.WorkerId));

        if (filter.statusIds != null && filter.statusIds.Any())
            query = query.Where(w => filter.statusIds.Contains(w.StatusId));

        return query;
    }

    public async Task<List<WorkProposalItem>> Getpaged(WorkProposalFilter filter)
    {
        var query = _context.WorkProposals.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "order" => filter.isDescending
                ? query.OrderByDescending(w => w.OrderId)
                : query.OrderBy(w => w.OrderId),
            "job" => filter.isDescending
                ? query.OrderByDescending(w => w.Work == null
                    ? string.Empty
                    : w.Work.Title)
                : query.OrderBy(w => w.Work == null
                    ? string.Empty
                    : w.Work.Title),
            "worker" => filter.isDescending
                ? query.OrderByDescending(w => w.Worker == null
                    ? string.Empty
                    : w.Worker.Surname)
                : query.OrderBy(w => w.Worker == null
                    ? string.Empty
                    : w.Worker.Surname),
            "status" => filter.isDescending
                ? query.OrderByDescending(w => w.Status == null
                    ? string.Empty
                    : w.Status.Name)
                : query.OrderBy(w => w.Status == null
                    ? string.Empty
                    : w.Status.Name),
            "date" => filter.isDescending
                ? query.OrderByDescending(w => w.Date)
                : query.OrderBy(w => w.Date),

            _ => filter.isDescending
                ? query.OrderByDescending(w => w.Id)
                : query.OrderBy(w => w.Id),
        };

        var projection = query.Select(w => new WorkProposalItem(
            w.Id,
            w.OrderId,
            w.Work == null
                ? string.Empty
                : w.Work.Title,
            w.Worker == null
                ? string.Empty
                : $"{w.Worker.Name} {w.Worker.Surname}",
            w.Status == null
                ? string.Empty
                : w.Status.Name,
            w.Date));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }


    public async Task<long> Create(WorkProposal workProposal)
    {
        var workProposalEntity = new WorkProposalEntity
        {
            OrderId = workProposal.OrderId,
            JobId = workProposal.JobId,
            WorkerId = workProposal.WorkerId,
            StatusId = workProposal.StatusId,
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

        if (statusId.HasValue) workProposal.StatusId = statusId.Value;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<long> AcceptProposal(long id)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id)
            ?? throw new Exception("Work proposal not found");

        workProposal.StatusId = ProposalStatusEnum.Accepted;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<long> RejectProposal(long id)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id)
            ?? throw new Exception("Work proposal not found");

        workProposal.StatusId = ProposalStatusEnum.Rejected;

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
}
