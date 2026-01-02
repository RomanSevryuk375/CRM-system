using CRMSystem.Core.DTOs;
using CRMSystem.Core.DTOs.AcceptanceImg;

namespace CRMSystem.Buisness.Abstractions;

public interface IAcceptanceImgService
{
    Task<long> CreateAccptanceImg(long AcceptanceId, FileItem file, string? description);
    Task<long> DeleteAccptanceImg(long id);
    Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter);
    Task<int> GetCountAccptnceImg(AcceptanceImgFilter filter);
    Task<(Stream FileStream, string ContentType)> GetImageStream(long id);
    Task<long> UpdateAccptanceImg(long id, string? filePath, string? description);
}