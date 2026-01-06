using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface ICarStatusRepository
{
    Task<List<CarStatusItem>> Get();
    Task<bool> Exists(long id);
}