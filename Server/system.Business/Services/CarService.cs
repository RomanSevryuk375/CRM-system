using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;
using CRMSystem.Core.Enums;

namespace CRMSystem.Business.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IClientRepository _clientsRepository;
    private readonly ICarStatusRepository _carStatusRepository;
    private readonly ILogger<CarService> _logger;

    public CarService(
        ICarRepository carRepository,
        IClientRepository clientsRepository,
        ICarStatusRepository carStatusRepository,
        ILogger<CarService> logger)
    {
        _carRepository = carRepository;
        _clientsRepository = clientsRepository;
        _carStatusRepository = carStatusRepository;
        _logger = logger;
    }

    public async Task<List<CarItem>> GetPagedCars(CarFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Car getting success");

        var car = await _carRepository.Get(filter, ct);

        _logger.LogInformation("Car getting success");

        return car;
    }

    public async Task<int> GetCountCars(CarFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Car getting count start");

        var count = await _carRepository.GetCount(filter, ct);

        _logger.LogInformation("Car getting count success");

        return count;
    }

    public async Task<CarItem> GetCarById(long id, CancellationToken ct)
    {
        _logger.LogInformation("Car getting by id start");

        var car = await _carRepository.GetById(id, ct);
        if (car is null)
        {
            _logger.LogError("Car{CarId} not found", id);
            throw new NotFoundException($"Car{id} not found");
        }

        _logger.LogInformation("Car getting by id success");

        return car;
    }

    public async Task<long> CreateCar(Car car, CancellationToken ct)
    {
        _logger.LogInformation("Creating car started");

        if (!await _clientsRepository.Exists(car.OwnerId, ct))
        {
            _logger.LogError("Client{ClientId} not found", car.OwnerId);
            throw new NotFoundException($"Client{car.OwnerId} not found");
        }

        if (!await _carStatusRepository.Exists((int)car.StatusId, ct)
        || car.StatusId is CarStatusEnum.AtWork)
        {
            _logger.LogError("Status{StatusId} not found or invalid status", (int)car.StatusId);
            throw new NotFoundException($"Car{(int)car.StatusId} not found or invalid status");
        }

        var Id = await _carRepository.Create(car, ct);

        _logger.LogInformation("Creating car success");

        return Id;
    }

    public async Task<long> UpdateCar(long id, CarUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating car start");

        var Id = await _carRepository.Update(id, model, ct);

        _logger.LogInformation("Updating car success");

        return Id;
    }

    public async Task<long> DeleteCar(long id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting car start");

        var Id = await _carRepository.Delete(id, ct);

        _logger.LogInformation("Deleting car success");

        return Id;
    }
}
