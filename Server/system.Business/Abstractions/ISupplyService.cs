using CRMSystem.Core.ProjectionModels.Supply;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface ISupplyService
{
    Task<long> CreateSupply(SupplyCreateModel createModel, CancellationToken ct);
    Task<long> DeleteSupply(long id, CancellationToken ct);
    Task<int> GetCountSupplies(SupplyFilter filter, CancellationToken ct);
    Task<List<SupplyItem>> GetPagedSupplies(SupplyFilter filter, CancellationToken ct);
}