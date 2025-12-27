using CRMSystem.Core.DTOs.Supply;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface ISypplyRepository
    {
        Task<long> Create(Supply supply);
        Task<long> Delete(long id);
        Task<List<SupplyItem>> GetPaged(SupplyFilter filter);
    }
}