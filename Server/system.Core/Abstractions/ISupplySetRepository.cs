using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISupplySetRepository
{
    Task<long> Create(SupplySet supplySet);
    Task<long> Delete(long id);
    Task<int> GetCount(SupplySetFilter filter);
    Task<List<SupplySetItem>> GetPaged(SupplySetFilter filter);
    Task<long> Update(long id, SupplySetUpdateModel model);
}