using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ICarRepository
    {
        Task<List<Car>> Get();
        Task<List<Car>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<List<Car>> GetByOwnerId(int ownerId);
        Task<List<Car>> GetPagedByOwnerId(int ownerId, int page, int limit);
        Task<int> GetCountByOwnerId(int ownerId);
        Task<List<Car>> GetById(List<int> carIds);
        Task<List<Car>> GetPagedById(List<int> carIds, int page, int limit);
        Task<int> GetCountById(List<int> carIds);
        Task<int> Create(Car car);
        Task<int> Delete(int id);
        Task<int> Update(int id, string? brand, string? model, int? yearOfManufacture, string? vinNumber, string? stateNumber, int? mileage);
    }
}