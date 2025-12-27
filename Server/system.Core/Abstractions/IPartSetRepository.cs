using CRMSystem.Core.DTOs.PartSet;
using CRMSystem.Core.Models;
using System.Threading.Tasks;

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
    Task<long> MoveFromProposalToOrder(long proposalId, long orderId);
    Task<long> DeleteProposedParts(long proposalId);
}