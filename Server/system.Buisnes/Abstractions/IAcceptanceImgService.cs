using CRMSystem.Core.DTOs.AcceptanceImg;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAcceptanceImgService
{
    Task<long> CreateAccptanceImg(AcceptanceImg acceptanceImg);
    Task<long> Delete(long id);
    Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter);
    Task<int> GetCountAccptnceImg(AcceptanceImgFilter filter);
    Task<long> Update(long id, string? filePath, string? description);
}