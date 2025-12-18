using CRMSystem.Core.DTOs.SupplySet;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISupplySetRepository
    {
        Task<long> Create(SupplySet supplySet);
        Task<long> Delete(long id);
        Task<int> GetCount(SupplySetFilter filter);
        Task<List<SupplySetItem>> GetPaged(SupplySetFilter filter);
        Task<long> Update(long id, SupplySetUpdateModel model);
    }
}