using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Core.Abstractions;

public interface ISupplySetRepository
{
    Task<long> Create(SupplySet supplySet, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(SupplySetFilter filter, CancellationToken ct);
    Task<List<SupplySetItem>> GetPaged(SupplySetFilter filter, CancellationToken ct);
    Task<long> Update(long id, SupplySetUpdateModel model, CancellationToken ct);
}