using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IUsedPartRepository
    {
        Task<List<UsedPart>> Get();
        Task<List<UsedPart>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<List<UsedPart>> GetByOrderId(List<int> orderIds);
        Task<List<UsedPart>> GetByPagedOrderId(List<int> orderIds, int page, int limit);
        Task<int> GetCountByOrderId(List<int> orderIds);
        Task<int> Create(UsedPart usedPart);
        Task<int> Delete(int id);
        Task<int> Update(int id, int? orderId, int? supplierId, string? name, string? article, decimal? quantity, decimal? unitPrice, decimal? sum);
    }
}