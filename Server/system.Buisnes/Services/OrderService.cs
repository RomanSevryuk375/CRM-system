using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly ICarRepository _carRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IStatusRepository statusRepository,
        ICarRepository carRepository)
    {
        _orderRepository = orderRepository;
        _statusRepository = statusRepository;
        _carRepository = carRepository;
    }

    public async Task<List<Order>> GetOrders()
    {
        return await _orderRepository.Get();
    }

    public async Task<List<OrderWithInfoDto>> GetOrderWithInfo()
    {
        var orders = await _orderRepository.Get();
        var cars = await _carRepository.Get();
        var statuses = await _statusRepository.Get();

        var response = (from o in orders
                        join c in cars on o.CarId equals c.Id
                        join s in statuses on o.StatusId equals s.Id
                        select new OrderWithInfoDto(
                            o.Id,
                            s.Name,
                            $"{c.Brand} {c.Model} ({c.StateNumber})",
                            o.Date,
                            o.Priority)).ToList();

        return response;
    }

    public async Task<int> CreateOrder(Order order)
    {
        return await _orderRepository.Create(order);
    }

    public async Task<int> UpdateOrder(int id, int statusId, int carId, DateTime date, string priority)
    {
        return await _orderRepository.Update(id, statusId, carId, date, priority);
    }

    public async Task<int> DeleteOrder(int id)
    {
        return await _orderRepository.Delete(id);
    }
}
