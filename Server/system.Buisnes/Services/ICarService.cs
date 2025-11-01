using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface ICarService
    {
        Task<int> CreateCar(Car car);
        Task<int> DeleteCar(int id);
        Task<List<Car>> GetAllCars();
        Task<List<CarWithOwnerDto>> GetCarsWithOwner();
        Task<int> UpdateCar(int id, string brand, string model, int? yearOfManufacture, string vinNumber, string stateNumber, int? mileage);
    }
}