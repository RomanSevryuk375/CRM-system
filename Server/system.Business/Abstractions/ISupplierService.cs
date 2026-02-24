using CRMSystem.Core.ProjectionModels.Supplier;

namespace CRMSystem.Business.Abstractions;

public interface ISupplierService
{
    Task<int> CreateSupplier(SupplierCreateModel createModel, CancellationToken ct);
    Task<int> DeleteSupplier(int id, CancellationToken ct);
    Task<List<SupplierItem>> GetSuppliers(CancellationToken ct);
    Task<int> UpdateSupplier(int id, SupplierUpdateModel model, CancellationToken ct);
}