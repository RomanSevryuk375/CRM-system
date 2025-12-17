using CRMSystem.Core.DTOs.Supplier;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISupplierRepository
    {
        Task<int> Create(Supplier supplier);
        Task<int> Delete(int id);
        Task<List<SupplierItem>> Get();
        Task<int> Update(int id, SupplierUpdateModel model);
    }
}