using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISupplierService
{
    Task<int> CreateSupplier(Supplier supplier, CancellationToken ct);
    Task<int> DeleteSupplier(int id, CancellationToken ct);
    Task<List<SupplierItem>> GetSuppliers(CancellationToken ct);
    Task<int> UpdateSupplier(int id, SupplierUpdateModel model, CancellationToken ct);
}