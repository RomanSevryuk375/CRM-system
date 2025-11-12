using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ICarRepository
    {
        Task<int> Create(Car car);
        Task<int> Delete(int id);
        Task<List<Car>> Get();
        Task<int> Update(int id, string brand, string model, int? yearOfManufacture, string vinNumber, string stateNumber, int? mileage);
        Task<List<Car>> GetByOwnerId(int ownerId);
        Task<List<Car>> GetById(List<int> carIds);
    }
}