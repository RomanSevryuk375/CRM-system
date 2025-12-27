using CRMSystem.Core.DTOs.AcceptanceImg;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories;

public interface IAcceptanceImgRepository
{
    Task<long> Create(AcceptanceImg acceptanceImg);
    Task<long> Delete(long id);
    Task<int> GetCount(AcceptanceImgFilter filter);
    Task<List<AcceptanceImgItem>> GetPaged(AcceptanceImgFilter filter);
    Task<long> Update(long id, string? filePath, string? description);
}