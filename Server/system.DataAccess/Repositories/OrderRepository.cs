using AutoMapper;
using AutoMapper.QueryableExtensions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SystemDbContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(
        SystemDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    private IQueryable<OrderEntity> ApplyFilter(IQueryable<OrderEntity> query, OrderFilter filter)
    {
        if (filter.StatusIds != null && filter.StatusIds.Any())
            query = query.Where(o => filter.StatusIds.Contains(o.StatusId));

        if (filter.PriorityIds != null && filter.PriorityIds.Any())
            query = query.Where(o => filter.PriorityIds.Contains(o.PriorityId));

        if (filter.CarIds != null && filter.CarIds.Any())
            query = query.Where(o => filter.CarIds.Contains(o.CarId));

        if (filter.OrderIds != null && filter.OrderIds.Any())
            query = query.Where(o => filter.OrderIds.Contains(o.Id));

        return query;
    }

    public async Task<List<OrderItem>> GetPaged(OrderFilter filter)
    {
        var query = _context.Orders.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "status" => filter.IsDescending
                ? query.OrderByDescending(o => o.Status == null
                    ? string.Empty
                    : o.Status.Name)
                : query.OrderBy(o => o.Status == null
                    ? string.Empty
                    : o.Status.Name),
            "car" => filter.IsDescending
                ? query.OrderByDescending(o => o.Car == null
                    ? string.Empty
                    : o.Car.Brand)
                : query.OrderBy(o => o.Car == null
                    ? string.Empty
                    : o.Car.Brand),
            "date" => filter.IsDescending
                ? query.OrderByDescending(o => o.Date)
                : query.OrderBy(o => o.Date),
            "priority" => filter.IsDescending
                ? query.OrderByDescending(o => o.OrderPriority == null
                    ? string.Empty
                    : o.OrderPriority.Name)
                : query.OrderBy(o => o.OrderPriority == null
                    ? string.Empty
                    : o.OrderPriority.Name),

            _ => filter.IsDescending
                ? query.OrderByDescending(o => o.Id)
                : query.OrderBy(o => o.Id),
        };

        return await query
            .ProjectTo<OrderItem>(_mapper.ConfigurationProvider)
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
    }

    public async Task<OrderItem?> GetByProposalId(long proposalId)
    {
        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.PriorityId == proposalId)
            .ProjectTo<OrderItem>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

    }

    public async Task<int> GetCount(OrderFilter filter)
    {
        var query = _context.Orders.AsNoTracking();
        query = ApplyFilter(query, filter);
        return await query.CountAsync();
    }

    public async Task<long> Create(Order order)
    {

        var orderEntitie = new OrderEntity
        {
            StatusId = (int)order.StatusId,
            CarId = order.CarId,
            Date = order.Date,
            PriorityId = (int)order.PriorityId
        };

        await _context.Orders.AddAsync(orderEntitie);
        await _context.SaveChangesAsync();

        return orderEntitie.Id;
    }

    public async Task<long> Update(long id, OrderPriorityEnum? priorityId)
    {
        var entity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("Order not found");

        if (priorityId.HasValue) entity.PriorityId = (int)priorityId.Value;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Complete(long id)
    {
        var entity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("Order not found");

        entity.StatusId = (int)OrderStatusEnum.Completed;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Close(long id)
    {
        var entity = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException("Order not found");

        entity.StatusId = (int)OrderStatusEnum.Closed;

        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<long> Delete(long id)
    {
        var order = await _context.Orders
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }

    public async Task<bool> Exists(long id)
    {
        return await _context.Orders
            .AnyAsync(x => x.Id == id);
    }

    public async Task<int?> GetStatus(long id)
    {
        return await _context.Orders
        .AsNoTracking()
        .Where(o => o.Id == id)
        .Select(o => (int?)o.StatusId)
        .FirstOrDefaultAsync();
    }

    public async Task<bool> PossibleToComplete(long id)
    {
        return !await _context.WorksInOrder
            .Where(w => w.OrderId == id)
            .AnyAsync(w => (w.StatusId == (int)WorkStatusEnum.InProgress 
                            || w.StatusId == (int)WorkStatusEnum.Pending));
    }

    public async Task<bool> PossibleToClose(long id)
    {
        return await _context.Bills
            .Where(b => b.OrderId == id)
            .AnyAsync(b => b.Amount <= 0);
    }
}
