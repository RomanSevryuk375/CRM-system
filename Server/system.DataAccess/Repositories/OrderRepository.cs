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

    public async Task<List<Order>> Get()
    {
        var orderEntities = await _context.Orders
            .AsNoTracking()
            .ToListAsync();

        var orders = orderEntities
            .Select(o => Order.Create(
                o.Id,
                o.StatusId,
                o.CarId,
                o.Date,
                o.Priority).order)
            .ToList();

        return orders;
    }
    
    public async Task<int> GetCount()
    {
        return await _context.Orders.CountAsync();
    }

    public async Task<List<Order>> GetById(List<int> orderIds)
    {
        var orderEntities = await _context.Orders
            .AsNoTracking()
            .Where(o => orderIds.Contains(o.Id) && (o.StatusId == 5 || o.StatusId == 7)) //not tested but if it disable it work good
            .ToListAsync();

        var orders = orderEntities
            .Select(o => Order.Create(
                o.Id,
                o.StatusId,
                o.CarId,
                o.Date,
                o.Priority).order)
            .ToList();

        return orders;
    }
    
    public async Task<int> GetCountById(List<int> orderIds)
    {
        return await _context.Orders.Where(o => orderIds.Contains(o.Id)).CountAsync();
    }

    public async Task<List<Order>> GetByCarId(List<int> carIds)
    {
        var orderEntities = await _context.Orders
            .AsNoTracking()
            .Where(o => carIds.Contains(o.CarId))
            .ToListAsync();

        var orders = orderEntities
            .Select(o => Order.Create(
                o.Id,
                o.StatusId,
                o.CarId,
                o.Date,
                o.Priority).order)
            .ToList();

        return orders;
    }

    public async Task<int> GetCountByCarId (List<int> carIds)
    {
        return await _context.Orders.Where(o => carIds.Contains(o.CarId)).CountAsync();
    }

    public async Task<int> Create(Order order)
    {
        var (_, error) = Order.Create(
            0,
            order.StatusId,
            order.CarId,
            order.Date,
            order.Priority);

        if (!string.IsNullOrEmpty(error))
            throw new ArgumentException($"Create exception Order: {error}");

        var orderEntitie = new OrderEntity
        {
            StatusId = order.StatusId,
            CarId = order.CarId,
            Date = order.Date,
            Priority = order.Priority
        };

        await _context.Orders.AddAsync(orderEntitie);
        await _context.SaveChangesAsync();

        return orderEntitie.Id;
    }

    public async Task<int> Update(int id, int? statusId, int? carId, DateTime? date, string? priority)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id)
            ?? throw new Exception("Order not found");

        if (statusId.HasValue)
            order.StatusId = statusId.Value;
        if (carId.HasValue)
            order.CarId = carId.Value;
        if (date.HasValue)
            order.Date = date.Value;
        if (!string.IsNullOrWhiteSpace(priority))
            order.Priority = priority;

        await _context.SaveChangesAsync();

        return order.Id;
    }

    public async Task<int> Delete(int id)
    {
        var order = await _context.Orders
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}
