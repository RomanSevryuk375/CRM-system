using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ICarRepository
{
    Task<long> Create(Car car);
    Task<long> Delete(long id);
    Task<List<CarItem>> Get(CarFilter filter);
    Task<int> GetCount(CarFilter filter);
    Task<long> Update(long id, CarUpdateModel model);
    Task<CarItem?> GetById(long id);
    Task<bool> Exists(long id);
}