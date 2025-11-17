using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IClientsRepository _clientsRepository;
    private readonly IWorkRepository _workRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IWorkerRepository _workerRepository;

    public CarService(
        ICarRepository carRepository,
        IClientsRepository clientsRepository,
        IWorkRepository workRepository,
        IOrderRepository orderRepository,
        IWorkerRepository workerRepository)
    {
        _carRepository = carRepository;
        _clientsRepository = clientsRepository;
        _workRepository = workRepository;
        _orderRepository = orderRepository;
        _workerRepository = workerRepository;
    }

    public async Task<List<Car>> GetAllCars(int page, int limit)
    {
        return await _carRepository.GetPaged(page, limit);
    }

    public async Task<int> GetCountAllCars()
    {
        return await _carRepository.GetCount();
    }

    public async Task<List<Car>> GetCarsByOwnerId(int userId, int page, int limit)
    {
        var clients = await _clientsRepository.GetClientByUserId(userId);
        var ownerId = clients.Select(c => c.Id).FirstOrDefault();

        return await _carRepository.GetPagedByOwnerId(ownerId, page, limit);
    }

    public async Task<int> GetCountCarsByOwnerId(int userId)
    {
        var clients = await _clientsRepository.GetClientByUserId(userId);
        var ownerId = clients.Select(c => c.Id).FirstOrDefault();

        return await _carRepository.GetCountByOwnerId(ownerId);
    }

    public async Task<List<Car>> GetCarsForWorker(int userId)
    {
        var workers = await _workerRepository.GetWorkerIdByUserId(userId);
        var workerIds = workers.Select(c => c.Id).ToList();
        var works = await _workRepository.GetByWorkerId(workerIds);
        var orderId = works.Select(works => works.OrderId).ToList();
        var orders = await _orderRepository.GetById(orderId);
        var carId = orders.Select(orders => orders.CarId).ToList();

        return await _carRepository.GetById(carId);
    }

    public async Task<List<CarWithOwnerDto>> GetCarsWithOwner(int page, int limit)
    {
        var cars = await _carRepository.GetPaged(page, limit);
        var clients = await _clientsRepository.Get();

        var response = (from c in cars
                        join cl in clients on c.OwnerId equals cl.Id
                        select new CarWithOwnerDto(
                            c.Id,
                            c.OwnerId,
                            $"{cl.Name} {cl.Surname}",
                            c.Brand,
                            c.Model,
                            c.YearOfManufacture,
                            c.VinNumber,
                            c.StateNumber,
                            c.Mileage)).ToList();

        return response;
    }

    public async Task<int> CreateCar(Car car)
    {
        return await _carRepository.Create(car);
    }

    public async Task<int> UpdateCar(int id, string brand, string model, int? yearOfManufacture, string vinNumber, string stateNumber, int? mileage)
    {
        return await _carRepository.Update(id, brand, model, yearOfManufacture, vinNumber, stateNumber, mileage);
    }

    public async Task<int> DeleteCar(int id)
    {
        return await _carRepository.Delete(id);
    }
}
