using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ICarRepository
{
    Task<long> Create(Car car, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<List<CarItem>> GetPaged(CarFilter filter, CancellationToken ct);
    Task<int> GetCount(CarFilter filter, CancellationToken ct);
    Task<long> Update(long id, CarUpdateModel model, CancellationToken ct);
    Task<CarItem?> GetById(long id, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}