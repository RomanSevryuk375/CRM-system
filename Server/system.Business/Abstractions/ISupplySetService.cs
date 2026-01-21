using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface ISupplySetService
{
    Task<long> CreateSupplySet(SupplySet supplySet, CancellationToken ct);
    Task<long> DeleteSupplySet(long id, CancellationToken ct);
    Task<int> GetCountSupplySets(SupplySetFilter filter, CancellationToken ct);
    Task<List<SupplySetItem>> GetPagedSupplySets(SupplySetFilter filter, CancellationToken ct);
    Task<long> UpdateSupplySet(long id, SupplySetUpdateModel model, CancellationToken ct);
}