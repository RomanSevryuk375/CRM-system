using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories;

public interface ICarStatusRepository
{
    Task<List<CarStatusItem>> Get();
    Task<bool> Exists(long id);
}