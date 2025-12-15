using CRMSystem.Core.DTOs.Car;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ICarRepository
    {
        Task<long> Create(Car car);
        Task<long> Delete(long id);
        Task<List<CarItem>> Get(CarFilter filter);
        Task<int> GetCount(CarFilter filter);
        Task<long> Update(long id, CarUpdateModel model);
    }
}