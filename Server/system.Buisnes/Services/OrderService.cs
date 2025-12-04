using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly ICarRepository _carRepository;
    private readonly IClientsRepository _clientsRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IWorkRepository _workRepository;

    public OrderService(
        IOrderRepository orderRepository,
        IStatusRepository statusRepository,
        ICarRepository carRepository,
        IClientsRepository clientsRepository,
        IWorkerRepository workerRepository,
        IWorkRepository workRepository)
    {
        _orderRepository = orderRepository;
        _statusRepository = statusRepository;
        _carRepository = carRepository;
        _clientsRepository = clientsRepository;
        _workerRepository = workerRepository;
        _workRepository = workRepository;
    }

    public async Task<List<Order>> GetPagedOrders(int page, int limit)
    {
        return await _orderRepository.GetPaged(page, limit);
    }

    public async Task<int> GetCountOrders()
    {
        return await _orderRepository.GetCount();
    }

    public async Task<List<OrderWithInfoDto>> GetOrderWithInfo(int page, int limit)
    {
        var orders = await _orderRepository.GetPaged(page, limit);
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

    public async Task<List<OrderWithInfoDto>> GetPagedUserOrders(int userId, int page, int limit)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        var ownerId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(ownerId);
        var carIds = cars.Select(c => c.Id).ToList();

        var orders = await _orderRepository.GetPagedByCarId(carIds, page, limit);

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

    public async Task<int> GetCountUserOrders(int userId)
    {
        var client = await _clientsRepository.GetClientByUserId(userId);
        var ownerId = client.Select(c => c.Id).FirstOrDefault();

        var cars = await _carRepository.GetByOwnerId(ownerId);
        var carIds = cars.Select(c => c.Id).ToList();

        return await _orderRepository.GetCountByCarId(carIds);
    }

    public async Task<List<OrderWithInfoDto>> GetWorkerOrders(int userId)
    {
        var worker = await _workerRepository.GetWorkerByUserId(userId);
        var workerId = worker.Select(w => w.Id).ToList();

        var works = await _workRepository.GetByWorkerId(workerId);
        var orderIds = works.Select(c => c.OrderId).ToList();

        var orders = await _orderRepository.GetById(orderIds);

        var statuses = await _statusRepository.Get();
        var cars = await _carRepository.Get();

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

    public async Task<int> UpdateOrder(int id, int? statusId, int? carId, DateTime? date, string? priority)
    {
        return await _orderRepository.Update(id, statusId, carId, date, priority);
    }

    public async Task<int> DeleteOrder(int id)
    {
        return await _orderRepository.Delete(id);
    }
}
