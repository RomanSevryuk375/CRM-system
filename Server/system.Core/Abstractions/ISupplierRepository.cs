using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISupplierRepository
    {
        Task<int> Create(Supplier supplier);
        Task<int> Delete(int id);
        Task<List<Supplier>> Get();
        Task<int> Update(int id, string name, string contacts);
    }
}