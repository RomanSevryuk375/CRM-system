using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkPropossalRepository : IWorkPropossalRepository
{
    private readonly SystemDbContext _context;

    public WorkPropossalRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<WorkProposal>> Get()
    {
        var workProposalEntities = await _context.WorkProposals
            .AsNoTracking()
            .ToListAsync();

        var workProposals = workProposalEntities
            .Select(wp => WorkProposal.Create(
                wp.Id,
                wp.OrderId,
                wp.WorkId,
                wp.ByWorker,
                wp.StatusId,
                wp.DecisionStatusId,
                wp.Date).workPropossal)
            .ToList();

        return workProposals;
    }

    public async Task<List<WorkProposal>> GetPaged(int page, int limit)
    {
        var workProposalEntities = await _context.WorkProposals
            .AsNoTracking()
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var workProposals = workProposalEntities
            .Select(wp => WorkProposal.Create(
                wp.Id,
                wp.OrderId,
                wp.WorkId,
                wp.ByWorker,
                wp.StatusId,
                wp.DecisionStatusId,
                wp.Date).workPropossal)
            .ToList();

        return workProposals;
    }

    public async Task<int> GetCount()
    {
        return await _context.WorkProposals.CountAsync();
    }

    public async Task<List<WorkProposal>> GetByOrderId(List<int> orderIds)
    {
        var workProposalEntities = await _context.WorkProposals
            .AsNoTracking()
            .Where(p => orderIds.Contains(p.OrderId))
            .ToListAsync();

        var workProposals = workProposalEntities
            .Select(wp => WorkProposal.Create(
                wp.Id,
                wp.OrderId,
                wp.WorkId,
                wp.ByWorker,
                wp.StatusId,
                wp.DecisionStatusId,
                wp.Date).workPropossal)
            .ToList();

        return workProposals;
    }

    public async Task<List<WorkProposal>> GetPagedByOrderId(List<int> orderIds, int page, int limit)
    {
        var workProposalEntities = await _context.WorkProposals
            .AsNoTracking()
            .Where(p => orderIds.Contains(p.OrderId))
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var workProposals = workProposalEntities
            .Select(wp => WorkProposal.Create(
                wp.Id,
                wp.OrderId,
                wp.WorkId,
                wp.ByWorker,
                wp.StatusId,
                wp.DecisionStatusId,
                wp.Date).workPropossal)
            .ToList();

        return workProposals;
    }

    public async Task<int> GetCountByOrderId(List<int> orderIds)
    {
        return await _context.WorkProposals.Where(p => orderIds.Contains(p.OrderId)).CountAsync();
    }

    public async Task<int> Create(WorkProposal workProposal)
    {
        var (_, error) = WorkProposal.Create(
            0,
            workProposal.OrderId,
            workProposal.WorkId,
            workProposal.ByWorker,
            workProposal.StatusId,
            workProposal.DecisionStatusId,
            workProposal.Date);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Work proposal: {error}");

        var workProposalEntity = new WorkProposalEntity
        {
            OrderId = workProposal.OrderId,
            WorkId = workProposal.WorkId,
            ByWorker = workProposal.ByWorker,
            StatusId = workProposal.StatusId,
            DecisionStatusId = workProposal.DecisionStatusId,
            Date = workProposal.Date
        };

        await _context.WorkProposals.AddAsync(workProposalEntity);
        await _context.SaveChangesAsync();

        return workProposalEntity.OrderId;
    }

    public async Task<int> Update(int id, int? orderId, int? workId, int? byWorker, int? statusId, int? decisionStatusId, DateTime? date)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Work proposal not found");

        if (orderId.HasValue)
            workProposal.OrderId = orderId.Value;
        if (workId.HasValue)
            workProposal.WorkId = workId.Value;
        if (byWorker.HasValue)
            workProposal.ByWorker = byWorker.Value;
        if (statusId.HasValue)
            workProposal.StatusId = statusId.Value;
        if (decisionStatusId.HasValue)
            workProposal.DecisionStatusId = decisionStatusId.Value;
        if (date.HasValue)
            workProposal.Date = DateTime.Now;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<int> AcceptProposal(int id)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id)
            ?? throw new Exception("Work proposal not found");

        workProposal.DecisionStatusId = 6;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<int> RejectProposal(int id)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(X => X.Id == id)
            ?? throw new Exception("Work proposal not found");

        workProposal.DecisionStatusId = 7;

        await _context.SaveChangesAsync();

        return workProposal.Id;
    }

    public async Task<int> Delete(int id)
    {
        var workProposal = await _context.WorkProposals
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
