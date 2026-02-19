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

    public async Task<long> CreateCar(CarCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating car started");

        if (!await clientsRepository.Exists(createModel.OwnerId, ct))
        {
            logger.LogError("Client{ClientId} not found", createModel.OwnerId);
            throw new NotFoundException($"Client{createModel.OwnerId} not found");
        }

        if (!await carStatusRepository.Exists((int)createModel.StatusId, ct)
        || createModel.StatusId is CarStatusEnum.AtWork)
        {
            logger.LogError("Status{StatusId} not found or invalid status", (int)createModel.StatusId);
            throw new NotFoundException($"Car{(int)createModel.StatusId} not found or invalid status");
        }
        
        var (car, errors) = Car.Create(
            0,
            createModel.OwnerId,
            createModel.StatusId,
            createModel.Brand,
            createModel.Model,
            createModel.YearOfManufacture,
            createModel.VinNumber,
            createModel.StateNumber,
            createModel.Mileage);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await carRepository.Create(car!, ct);

        logger.LogInformation("Creating car success");

        return id;
    }

    public async Task<long> UpdateCar(long id, CarUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating car start");

        var carId = await carRepository.Update(id, model, ct);

        logger.LogInformation("Updating car success");

        return carId;
    }

    public async Task<long> DeleteCar(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting car start");

        var carId = await carRepository.Delete(id, ct);

        logger.LogInformation("Deleting car success");

        return carId;
    }
}
