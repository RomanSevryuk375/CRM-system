using CRMSystem.Core.DTOs.Order;
using CRMSystem.Core.DTOs.Part;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IPartRepository
    {
        Task<long> Create(Part part);
        Task<long> Delete(long id);
        Task<int> GetCount(PartFilter filter);
        Task<List<PartItem>> GetPaged(PartFilter filter);
        Task<long> Update(long id, PartUpdateModel model);
    }
}