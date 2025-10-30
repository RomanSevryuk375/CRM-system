using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IUsedPartRepository
    {
        Task<int> Create(UsedPart usedPart);
        Task<int> Delete(int id);
        Task<List<UsedPart>> Get();
        Task<int> Update(int id, int? orderId, int? supplierId, string name, string article, decimal? quantity, decimal? unitPrice, decimal? sum);
    }
}