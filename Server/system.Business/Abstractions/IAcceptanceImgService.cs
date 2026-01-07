// Ignore Spelling: Img

using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AccetanceImg;

namespace CRMSystem.Business.Abstractions;

public interface IAcceptanceImgService
{
    Task<long> CreateAcceptanceImg(long AcceptanceId, FileItem file, string? description);
    Task<long> DeleteAcceptanceImg(long id);
    Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter);
    Task<int> GetCountAcceptanceImg(AcceptanceImgFilter filter);
    Task<(Stream FileStream, string ContentType)> GetImageStream(long id);
    Task<long> UpdateAcceptanceImg(long id, string? filePath, string? description);
}