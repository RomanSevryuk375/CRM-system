using CRMSystem.Core.ProjectionModels.CarStatus;

namespace CRMSystem.Core.Abstractions;

public interface ICarStatusRepository
{
    Task<CarStatusItem?> GetById(int id, CancellationToken ct);
    Task<List<CarStatusItem>> Get(CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}