using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface ISupplierService
    {
        Task<int> CreateSupplier(Supplier supplier);
        Task<int> DeleteSupplier(int id);
        Task<List<Supplier>> GetSupplier();
        Task<int> UpdateSupplier(int id, string name, string contacts);
    }
}