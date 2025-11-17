using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ITaxRepository
    {
        Task<List<Tax>> Get();
        Task<int> GetCount();
        Task<int> Create(Tax tax);
        Task<int> Delete(int id);
        Task<int> Update(int id, string name, decimal? rate, string type);
    }
}