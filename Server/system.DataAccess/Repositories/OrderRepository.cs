using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.DataAccess.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SystemDbContext _context;

    public OrderRepository(SystemDbContext context)
    {
        _context = context;
    }

    private IQueryable<OrderEntity> ApplyFilter(IQueryable<OrderEntity> query, OrderFilter filter)
    {
        if (filter.statusIds != null && filter.statusIds.Any())
            query = query.Where(o => filter.statusIds.Contains(o.StatusId));

        if (filter.priorityIds != null && filter.priorityIds.Any())
            query = query.Where(o => filter.priorityIds.Contains(o.PriorityId));

        if (filter.carIds != null && filter.carIds.Any())
            query = query.Where(o => filter.carIds.Contains(o.CarId));

        if (filter.orderIds != null && filter.orderIds.Any())
            query = query.Where(o => filter.orderIds.Contains(o.Id));

        return query;
    }

    public async Task<List<OrderItem>> GetPaged(OrderFilter filter)
    {
        var query = _context.Orders.AsNoTracking();
        query = ApplyFilter(query, filter);

        query = filter.SortBy?.ToLower().Trim() switch
        {
            "status" => filter.isDescending
                ? query.OrderByDescending(o => o.Status == null
                    ? string.Empty
                    : o.Status.Name)
                : query.OrderBy(o => o.Status == null
                    ? string.Empty
                    : o.Status.Name),
            "car" => filter.isDescending
                ? query.OrderByDescending(o => o.Car == null
                    ? string.Empty
                    : o.Car.Brand)
                : query.OrderBy(o => o.Car == null
                    ? string.Empty
                    : o.Car.Brand),
            "date" => filter.isDescending
                ? query.OrderByDescending(o => o.Date)
                : query.OrderBy(o => o.Date),
            "priority" => filter.isDescending
                ? query.OrderByDescending(o => o.OrderPriority == null
                    ? string.Empty
                    : o.OrderPriority.Name)
                : query.OrderBy(o => o.OrderPriority == null
                    ? string.Empty
                    : o.OrderPriority.Name),

            _ => filter.isDescending
                ? query.OrderByDescending(o => o.Id)
                : query.OrderBy(o => o.Id),
        };

        var projection = query.Select(o => new OrderItem(
            o.Id,
            o.Status == null
                ? string.Empty
                : o.Status.Name,
            o.StatusId,
            o.Car == null
                ? string.Empty
                : $"{o.Car.Brand} ({o.Car.StateNumber})",
            o.CarId,
            o.Date,
            o.OrderPriority == null
                ? string.Empty
                : o.OrderPriority.Name,
            o.PriorityId));

        return await projection
            .Skip((filter.Page - 1) * filter.Limit)
            .Take(filter.Limit)
            .ToListAsync();
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
            ?? throw new Exception("Order not found");

        if (priorityId.HasValue) entity.PriorityId = (int)priorityId.Value;

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
        .Select(o => o.StatusId)
        .FirstOrDefaultAsync();
    }
}
