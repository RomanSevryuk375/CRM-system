using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface ICarService
    {
        Task<List<Car>> GetAllCars(int page, int limit);
        Task<int> GetCountAllCars();
        Task<List<CarWithOwnerDto>> GetCarsWithOwner(int page, int limit);
        Task<int> GetCountCarsByOwnerId(int userId);
        Task<List<Car>> GetCarsByOwnerId(int userId, int page, int limit);
        Task<List<Car>> GetCarsForWorker(int userId);
        Task<int> CreateCar(Car car);
        Task<int> DeleteCar(int id);
        Task<int> UpdateCar(int id, string? brand, string? model, int? yearOfManufacture, string? vinNumber, string? stateNumber, int? mileage);
    }
}