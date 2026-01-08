using CRMSystem.Core.ProjectionModels.Acceptance;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IAcceptanceRepository
{
    Task<long> Create(Acceptance acceptance, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(AcceptanceFilter filter, CancellationToken ct);
    Task<List<AcceptanceItem>> GetPaged(AcceptanceFilter filter, CancellationToken ct);
    Task<long> Update(long id, AcceptanceUpdateModel model, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}