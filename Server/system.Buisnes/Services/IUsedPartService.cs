using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IUsedPartService
    {
        Task<List<UsedPart>> GetPagedUsedPart(int page, int limit);
        Task<List<UsedPartWithInfoDto>> GetPagedUsedPartWithInfo(int page, int limit);
        Task<int> GetCountUsedPart();
        Task<List<UsedPartWithInfoDto>> GetPagedWorkerUsedPart(int userId, int page, int limit);
        Task<int> GetCountWorkerUsedPart(int userId);
        Task<int> CreateUsedPart(UsedPart usedPart);
        Task<int> DeleteUsedPart(int id);
        Task<int> UpdateUsedPart(int id, int? orderId, int? supplierId, string? name, string? article, decimal? quantity, decimal? unitPrice, decimal? sum);
    }
}