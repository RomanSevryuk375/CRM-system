using CRMSystem.Core.ProjectionModels.Acceptance;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IAcceptanceService
{
    Task<long> CreateAcceptance(Acceptance acceptance, CancellationToken ct);
    Task<long> DeleteAcceptance(long id, CancellationToken ct);
    Task<int> GetCountAcceptance(AcceptanceFilter filter, CancellationToken ct);
    Task<List<AcceptanceItem>> GetPagedAcceptance(AcceptanceFilter filter, CancellationToken ct);
    Task<long> UpdateAcceptance(long id, AcceptanceUpdateModel model, CancellationToken ct);
}