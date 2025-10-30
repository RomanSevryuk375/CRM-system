using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

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

    public async Task<int> Update(
        int id,
        int? statusId,
        int? carId,
        DateTime? date,
        string priority)
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
