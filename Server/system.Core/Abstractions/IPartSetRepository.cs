using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Core.Abstractions;

public interface IPartSetRepository
{
    Task<long> Create(PartSet partSet, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<List<PartSetItem>> GetPaged(PartSetFilter filter, CancellationToken ct);
    Task<int> GetCount(PartSetFilter filter, CancellationToken ct);
    Task<long> Update(long id, PartSetUpdateModel model, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
    Task<List<PartSetItem>> GetByOrderId(long orderId, CancellationToken ct);
    Task<PartSetItem?> GetById(long id, CancellationToken ct);
    Task<long> MoveFromProposalToOrder(long proposalId, long orderId, CancellationToken ct);
    Task<long> DeleteProposedParts(long proposalId, CancellationToken ct);
}