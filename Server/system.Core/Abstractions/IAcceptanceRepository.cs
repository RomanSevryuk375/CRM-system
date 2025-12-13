using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IAcceptanceRepository
    {
        Task<long> Create(Acceptance acceptance);
        Task<long> Delete(long id);
        Task<long> GetCount(AcceptanceFilter filter);
        Task<List<AcceptanceItem>> GetPaged(AcceptanceFilter filter);
        Task<long> Update(long id, AcceptanceUpdateModel model);
    }
}