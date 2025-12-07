using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class WorkRepository : IWorkRepository
{
    private readonly SystemDbContext _context;

    public WorkRepository(SystemDbContext context)
    {
        _context = context;
    }

    public async Task<List<Work>> Get()
    {
        var workEntities = await _context.Works
            .AsNoTracking()
            .ToListAsync();

        var work = workEntities
            .Select(w => Work.Create(
                w.Id,
                w.OrderId,
                w.JobId,
                w.WorkerId,
                w.TimeSpent,
                w.StatusId).work)
            .ToList();

        return work;
    }

    public async Task<List<Work>> GetPaged(int page, int limit)
    {
        var workEntities = await _context.Works
            .AsNoTracking()
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var work = workEntities
            .Select(w => Work.Create(
                w.Id,
                w.OrderId,
                w.JobId,
                w.WorkerId,
                w.TimeSpent,
                w.StatusId).work)
            .ToList();

        return work;
    }

    public async Task<int> GetCount()
    {
        return await _context.Works.CountAsync();
    }

    public async Task<List<Work>> GetByWorkerId(List<int> workerIds)
    {
        var workEntities = await _context.Works
            .AsNoTracking()
            .Where(w => workerIds.Contains(w.WorkerId))
            .ToListAsync();

        var work = workEntities
            .Select(w => Work.Create(
                w.Id,
                w.OrderId,
                w.JobId,
                w.WorkerId,
                w.TimeSpent,
                w.StatusId).work)
            .ToList();

        return work;
    }

    public async Task<int> GetCountByWorkerId(List<int> workerIds)
    {
        return await _context.Works.Where(w => workerIds.Contains(w.WorkerId)).CountAsync();
    }

    public async Task<List<Work>> GetPagedByWorkerId(List<int> workerIds, int page, int limit)
    {
        var workEntities = await _context.Works
            .AsNoTracking()
            .Where(w => workerIds.Contains(w.WorkerId))
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        var work = workEntities
            .Select(w => Work.Create(
                w.Id,
                w.OrderId,
                w.JobId,
                w.WorkerId,
                w.TimeSpent,
                w.StatusId).work)
            .ToList();

        return work;
    }

    public async Task<List<Work>> GetByOrderId(List<int> orderId)
    {
        var workEntities = await _context.Works
            .AsNoTracking()
            .Where(w => orderId.Contains(w.OrderId))
            .ToListAsync();

        var work = workEntities
            .Select(w => Work.Create(
                w.Id,
                w.OrderId,
                w.JobId,
                w.WorkerId,
                w.TimeSpent,
                w.StatusId).work)
            .ToList();

        return work;
    }

    public async Task<int> Create(Work work)
    {
        var (_, error) = Work.Create(
            0,
            work.OrderId,
            work.JobId,
            work.WorkerId,
            work.TimeSpent,
            work.StatusId);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Work: {error}");

        var workEntity = new WorkInOrderEntity
        {
            OrderId = work.OrderId,
            JobId = work.JobId,
            WorkerId = work.WorkerId,
            TimeSpent = work.TimeSpent,
            StatusId = work.StatusId
        };

        await _context.Works.AddAsync(workEntity);
        await _context.SaveChangesAsync();

        return workEntity.Id;
    }

    public async Task<int> Update(int id, int? orderId, int? jobId, int? workerId, decimal? timeSpent, int? statusId)
    {
        var work = await _context.Works.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Work not found");

        if (orderId.HasValue)
            work.OrderId = orderId.Value;
        if (jobId.HasValue)
            work.JobId = jobId.Value;
        if (workerId.HasValue)
            work.WorkerId = workerId.Value;
        if (timeSpent.HasValue)
            work.TimeSpent = timeSpent.Value;
        if (statusId.HasValue)
            work.StatusId = statusId.Value;

        await _context.SaveChangesAsync();

        return work.Id;
    }

    public async Task<int> Delete(int id)
    {
        var work = await _context.Works
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
