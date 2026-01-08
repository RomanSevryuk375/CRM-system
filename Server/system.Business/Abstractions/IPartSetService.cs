using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IPartSetService
{
    Task<long> AddToPartSet(PartSet partSet, CancellationToken ct);
    Task<long> DeleteFromPartSet(long id, CancellationToken ct);
    Task<List<PartSetItem>> GetPagedPartSets(PartSetFilter filter, CancellationToken ct);
    Task<int> GetCountPartSets(PartSetFilter filter, CancellationToken ct);
    Task<List<PartSetItem>> GetPartSetsByOrderId(long orderId, CancellationToken ct);
    Task<long> UpdatePartSet(long id, PartSetUpdateModel model, CancellationToken ct);
    Task<PartSetItem> GetPartSetById(long id, CancellationToken ct);
}