using CRMSystem.Core.ProjectionModels.SupplySet;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface ISupplySetService
{
    Task<long> CreateSupplySet(SupplySetCreateModel createModel, CancellationToken ct);
    Task<long> DeleteSupplySet(long id, CancellationToken ct);
    Task<int> GetCountSupplySets(SupplySetFilter filter, CancellationToken ct);
    Task<List<SupplySetItem>> GetPagedSupplySets(SupplySetFilter filter, CancellationToken ct);
    Task<long> UpdateSupplySet(long id, SupplySetUpdateModel model, CancellationToken ct);
}