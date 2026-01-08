using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ICarService
{
    Task<long> CreateCar(Car car, CancellationToken ct);
    Task<long> DeleteCar(long id, CancellationToken ct);
    Task<CarItem> GetCarById(long id, CancellationToken ct);
    Task<int> GetCountCars(CarFilter filter, CancellationToken ct);
    Task<List<CarItem>> GetPagedCars(CarFilter filter, CancellationToken ct);
    Task<long> UpdateCar(long id, CarUpdateModel model, CancellationToken ct);
}