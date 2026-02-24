using CRMSystem.Core.ProjectionModels.Acceptance;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IAcceptanceService
{
    Task<long> CreateAcceptance(AcceptanceCreateModel createModel, CancellationToken ct);
    Task<long> DeleteAcceptance(long id, CancellationToken ct);
    Task<int> GetCountAcceptance(AcceptanceFilter filter, CancellationToken ct);
    Task<AcceptanceItem> GetAcceptanceById(int id, CancellationToken ct);
    Task<List<AcceptanceItem>> GetPagedAcceptance(AcceptanceFilter filter, CancellationToken ct);
    Task<long> UpdateAcceptance(long id, AcceptanceUpdateModel model, CancellationToken ct);
}