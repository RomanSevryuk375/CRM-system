using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IClientsRepository _clientsRepository;

    public CarService(ICarRepository carRepository, IClientsRepository clientsRepository)
    {
        _carRepository = carRepository;
        _clientsRepository = clientsRepository;
    }

    public async Task<List<Car>> GetAllCars()
    {
        return await _carRepository.Get();
    }

    public async Task<List<CarWithOwnerDto>> GetCarsWithOwner()
    {
        var cars = await _carRepository.Get();
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
