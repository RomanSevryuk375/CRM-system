using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> Get();
        Task<List<Supplier>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<int> Create(Supplier supplier);
        Task<int> Delete(int id);
        Task<int> Update(int id, string? name, string? contacts);
    }
}