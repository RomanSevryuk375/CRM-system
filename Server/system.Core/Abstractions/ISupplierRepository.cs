using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISupplierRepository
{
    Task<int> Create(Supplier supplier, CancellationToken ct);
    Task<int> Delete(int id, CancellationToken ct);
    Task<List<SupplierItem>> Get(CancellationToken ct);
    Task<int> Update(int id, SupplierUpdateModel model, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
    Task<bool> ExistsByName(string name, CancellationToken ct);
}