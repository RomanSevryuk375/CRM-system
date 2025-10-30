using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ITaxRepository
    {
        Task<int> Create(Tax tax);
        Task<int> Delete(int id);
        Task<List<Tax>> Get();
        Task<int> Update(int id, string name, decimal? rate, string type);
    }
}