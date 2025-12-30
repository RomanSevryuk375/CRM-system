using CRMSystem.Core.DTOs.Acceptance;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAcceptanceService
{
    Task<long> CreateAcceptance(Acceptance acceptance);
    Task<long> DeleteAcceptance(long id);
    Task<int> GetCountAcceptance(AcceptanceFilter filter);
    Task<List<AcceptanceItem>> GetPagedAcceptance(AcceptanceFilter filter);
    Task<long> UpdateAcceptance(long id, AcceptanceUpdateModel model);
}