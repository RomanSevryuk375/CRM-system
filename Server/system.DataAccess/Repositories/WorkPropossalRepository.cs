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
    }

    public async Task<int> Update(
        int id,
        int? orderId,
        int? workId,
        int? byWorker,
        int? statusId,
        int? decisionStatusId,
        DateTime? date)
    {
        var workProposal = await _context.WorkProposals.SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Expence not found");

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

    public async Task<int> Delete(int id)
    {
        var workProposal = await _context.WorkProposals
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
