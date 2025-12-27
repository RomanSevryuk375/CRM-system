using CRMSystem.Core.DTOs.SupplySet;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface ISupplySetService
{
    Task<long> CreateSupplySet(SupplySet supplySet);
    Task<long> DeleteSupplySet(long id);
    Task<int> GetCountSupplySets(SupplySetFilter filter);
    Task<List<SupplySetItem>> GetPagetSupplySets(SupplySetFilter filter);
    Task<long> UpdateSupplySet(long id, SupplySetUpdateModel model);
}