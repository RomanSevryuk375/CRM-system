using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using CRMSystem.Core.Exceptions;
using Shared.Filters;

namespace CRMSystem.DataAccess.Repositories;

public class WorkInOrderRepository : IWorkInOrderRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public WorkInOrderRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private static IQueryable<WorkInOrderEntity> ApplyFilter(IQueryable<WorkInOrderEntity> query, WorkInOrderFilter filter)
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

    public async Task<List<WorkInOrderItem>> GetPaged(WorkInOrderFilter filter, CancellationToken ct)
    {
        var query = _context.WorksInOrder.AsNoTracking();
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
                ? query.OrderByDescending(w => w.WorkInOrderStatus == null
                    ? string.Empty
                    : w.WorkInOrderStatus.Name)
                : query.OrderBy(w => w.WorkInOrderStatus == null
                    ? string.Empty
                    : w.WorkInOrderStatus.Name),
            "timespent" => filter.IsDescending
                ? query.OrderByDescending(w => w.TimeSpent)
                : query.OrderBy(w => w.TimeSpent),

            _ => filter.IsDescending
                ? query.OrderByDescending(w => w.Id)
                : query.OrderBy(w => w.Id),
        };

        return await query
            .ProjectTo<WorkInOrderItem>(_mapper.ConfigurationProvider, ct)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync(ct);
    }

    public async Task<int> GetCount(WorkInOrderFilter filter, CancellationToken ct)
    {
        var query = _context.WorksInOrder.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync(ct);
    }

    public async Task<List<WorkInOrderItem>> GetByOrderId(long orderId, CancellationToken ct)
    {
        var worksInOrder = await _context.WorksInOrder
            .AsNoTracking()
            .Where(w => w.OrderId == orderId)
            .ProjectTo<WorkInOrderItem>(_mapper.ConfigurationProvider, ct)
            .ToListAsync(ct);

        return worksInOrder;
    }

    public async Task<long> Create(WorkInOrder workInOrder, CancellationToken ct)
    {
        var workInOrderEntity = new WorkInOrderEntity
        {
            OrderId = workInOrder.OrderId,
            JobId = workInOrder.JobId,
            WorkerId = workInOrder.WorkerId,
            StatusId = (int)workInOrder.StatusId,
            TimeSpent = workInOrder.TimeSpent,
        };

        await _context.WorksInOrder.AddAsync(workInOrderEntity, ct);
        await _context.SaveChangesAsync(ct);

        return workInOrderEntity.Id;
    }

    public async Task<long> Update(long id, WorkInOrderUpdateModel model, CancellationToken ct)
    {
        var entity = await _context.WorksInOrder.FirstOrDefaultAsync(x => x.Id == id, ct)
            ?? throw new NotFoundException("Work in order not found");

        if (model.WorkerId.HasValue) entity.WorkerId = model.WorkerId.Value;
        if (model.StatusId.HasValue) entity.StatusId = (int)model.StatusId.Value;
        if (model.TimeSpent.HasValue) entity.TimeSpent = model.TimeSpent.Value;

        await _context.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<long> Delete(long id, CancellationToken ct)
    {
        var entity = await _context.WorksInOrder
            .Where(w => w.Id == id)
            .ExecuteDeleteAsync(ct);

        return id;
    }
}
