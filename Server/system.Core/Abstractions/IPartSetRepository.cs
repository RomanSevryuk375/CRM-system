using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IPartSetRepository
{
    Task<long> Create(PartSet partSet);
    Task<long> Delete(long id);
    Task<List<PartSetItem>> GetPaged(PartSetFilter filter);
    Task<int> GetCount(PartSetFilter filter);
    Task<long> Update(long id, PartSetUpdateModel model);
    Task<bool> Exists(long id);
    Task<List<PartSetItem>> GetByOrderId(long orderId);
    Task<PartSetItem?> GetById(long id);
    Task<long> MoveFromProposalToOrder(long proposalId, long orderId);
    Task<long> DeleteProposedParts(long proposalId);
}