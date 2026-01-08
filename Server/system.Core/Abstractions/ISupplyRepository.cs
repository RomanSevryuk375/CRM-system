using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface ISupplyRepository
{
    Task<long> Create(Supply supply, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<List<SupplyItem>> GetPaged(SupplyFilter filter, CancellationToken ct);
    Task<int> GetCount(SupplyFilter filter, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}