using CRMSystem.Core.DTOs.PartSet;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IPartSetService
{
    Task<long> AddtoPartSet(PartSet partSet);
    Task<long> DeleteFromPartSet(long id);
    Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter);
    Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId);
    Task<long> UpdatePartSet(long id, PartSetUpdateModel model);
    Task<PartSetItem> GetPartSetById(long id);
}