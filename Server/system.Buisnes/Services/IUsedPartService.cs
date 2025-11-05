using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IUsedPartService
    {
        Task<int> CreateUsedPart(UsedPart usedPart);
        Task<int> DeleteUsedPart(int id);
        Task<List<UsedPart>> GetUsedPart();
        Task<List<UsedPartWithInfoDto>> GetUsedPartWithInfo();
        Task<int> UpdateUsedPart(int id, int orderId, int supplierId, string name, string article, decimal quantity, decimal unitPrice, decimal sum);
    }
}