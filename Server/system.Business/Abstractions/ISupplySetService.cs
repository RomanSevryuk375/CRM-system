using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface ISupplySetService
{
    Task<long> CreateSupplySet(SupplySet supplySet);
    Task<long> DeleteSupplySet(long id);
    Task<int> GetCountSupplySets(SupplySetFilter filter);
    Task<List<SupplySetItem>> GetPagedSupplySets(SupplySetFilter filter);
    Task<long> UpdateSupplySet(long id, SupplySetUpdateModel model);
}