using CRMSystem.Core.ProjectionModels.Acceptance;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IAcceptanceService
{
    Task<long> CreateAcceptance(Acceptance acceptance);
    Task<long> DeleteAcceptance(long id);
    Task<int> GetCountAcceptance(AcceptanceFilter filter);
    Task<List<AcceptanceItem>> GetPagedAcceptance(AcceptanceFilter filter);
    Task<long> UpdateAcceptance(long id, AcceptanceUpdateModel model);
}