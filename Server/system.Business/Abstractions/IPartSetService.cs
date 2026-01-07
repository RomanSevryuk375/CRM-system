using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPartSetService
{
    Task<long> AddToPartSet(PartSet partSet);
    Task<long> DeleteFromPartSet(long id);
    Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter);
    Task<int> GetCountPartSets(PartSetFilter filter);
    Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId);
    Task<long> UpdatePartSet(long id, PartSetUpdateModel model);
    Task<PartSetItem> GetPartSetById(long id);
}