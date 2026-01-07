using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISupplierService
{
    Task<int> CreateSupplier(Supplier supplier);
    Task<int> DeleteSupplier(int id);
    Task<List<SupplierItem>> GetSuppliers();
    Task<int> UpdateSupplier(int id, SupplierUpdateModel model);
}