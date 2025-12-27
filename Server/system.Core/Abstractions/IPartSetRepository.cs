using CRMSystem.Core.DTOs.PartSet;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IPartSetRepository
{
    Task<long> Create(PartSet partSet);
    Task<long> Delete(long id);
    Task<List<PartSetItem>> GetPaged(PartSetFilter filter);
    Task<long> Update(long id, PartSetUpdateModel model);
    Task<bool> Exists(long id);
    Task<List<PartSetItem>> GetByOrderId(long orderId);
    Task<PartSetItem?> GetById(long id);
}