// Ignore Spelling: Img

using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AcceptanceImg;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IAcceptanceImgService
{
    Task<long> CreateAcceptanceImg(long acceptanceId, FileItem file, string? description, CancellationToken ct);
    Task<long> DeleteAcceptanceImg(long id, CancellationToken ct);
    Task<List<AcceptanceImgItem>> GetAcceptanceIng(AcceptanceImgFilter filter, CancellationToken ct);
    Task<AcceptanceImgItem> GetAcceptanceImgById(long id, CancellationToken ct);
    Task<int> GetCountAcceptanceImg(AcceptanceImgFilter filter, CancellationToken ct);
    Task<(Stream fileStream, string contentType)> GetImageStream(long id, CancellationToken ct);
    Task<long> UpdateAcceptanceImg(long id, string? filePath, string? description, CancellationToken ct);
}