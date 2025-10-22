using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _carRepository;

    public CarService(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<List<Car>> GetCars()
    {
        return await _carRepository.Get();
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
