using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISupplierRepository
{
    Task<int> Create(Supplier supplier);
    Task<int> Delete(int id);
    Task<List<SupplierItem>> Get();
    Task<int> Update(int id, SupplierUpdateModel model);
    Task<bool> Exists(int id);
    Task<bool> ExistsByName(string name);
}