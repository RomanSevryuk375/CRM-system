using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface ICarStatusRepository
{
    Task<List<CarStatusItem>> Get(CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}