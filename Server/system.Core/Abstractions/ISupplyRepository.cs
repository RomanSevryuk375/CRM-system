using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISupplyRepository
{
    Task<long> Create(Supply supply);
    Task<long> Delete(long id);
    Task<List<SupplyItem>> GetPaged(SupplyFilter filter);
    Task<int> GetCount(SupplyFilter filter);
    Task<bool> Exists(long id);
}