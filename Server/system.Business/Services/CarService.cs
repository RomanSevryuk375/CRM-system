using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class CarService(
    ICarRepository carRepository,
    IClientRepository clientsRepository,
    ICarStatusRepository carStatusRepository,
    IUserContext userContext,
    ILogger<CarService> logger) : ICarService
{
    public async Task<List<CarItem>> GetPagedCars(CarFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Car getting success");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { OwnerIds = [userContext.ProfileId] };
        }

        var car = await carRepository.GetPaged(filter, ct);

        logger.LogInformation("Car getting success");

        return car;
    }

    public async Task<int> GetCountCars(CarFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Car getting count start");

        var count = await carRepository.GetCount(filter, ct);

        logger.LogInformation("Car getting count success");

        return count;
    }

    public async Task<CarItem> GetCarById(long id, CancellationToken ct)
    {
        logger.LogInformation("Car getting by id start");

        var car = await carRepository.GetById(id, ct);
        if (car is null)
        {
            logger.LogError("Car{CarId} not found", id);
            throw new NotFoundException($"Car{id} not found");
        }

        logger.LogInformation("Car getting by id success");

        return car;
    }

    public async Task<long> CreateCar(Car car, CancellationToken ct)
    {
        logger.LogInformation("Creating car started");

        if (!await clientsRepository.Exists(car.OwnerId, ct))
        {
            logger.LogError("Client{ClientId} not found", car.OwnerId);
            throw new NotFoundException($"Client{car.OwnerId} not found");
        }

        if (!await carStatusRepository.Exists((int)car.StatusId, ct)
        || car.StatusId is CarStatusEnum.AtWork)
        {
            logger.LogError("Status{StatusId} not found or invalid status", (int)car.StatusId);
            throw new NotFoundException($"Car{(int)car.StatusId} not found or invalid status");
        }

        var Id = await carRepository.Create(car, ct);

        logger.LogInformation("Creating car success");

        return Id;
    }

    public async Task<long> UpdateCar(long id, CarUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating car start");

        var Id = await carRepository.Update(id, model, ct);

        logger.LogInformation("Updating car success");

        return Id;
    }

    public async Task<long> DeleteCar(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting car start");

        var Id = await carRepository.Delete(id, ct);

        logger.LogInformation("Deleting car success");

        return Id;
    }
}
