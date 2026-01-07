using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ICarService
{
    Task<long> CreateCar(Car car);
    Task<long> DeleteCar(long id);
    Task<CarItem> GetCarById(long id);
    Task<int> GetCountCars(CarFilter filter);
    Task<List<CarItem>> GetPagedCars(CarFilter filter);
    Task<long> UpdateCar(long id, CarUpdateModel model);
}