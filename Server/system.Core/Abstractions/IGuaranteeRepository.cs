using CRMSystem.Core.DTOs.Guarantee;
using CRMSystem.DataAccess.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IGuaranteeRepository
    {
        Task<long> Create(Guarantee guarantee);
        Task<long> Delete(long id);
        Task<int> GetCount(GuaranteeFilter filter);
        Task<List<GuaranteeItem>> GetPaged(GuaranteeFilter filter);
        Task<long> Update(long id, string? descroption);
    }
}