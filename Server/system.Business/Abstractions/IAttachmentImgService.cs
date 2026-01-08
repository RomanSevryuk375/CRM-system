// Ignore Spelling: Img

using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AttachmentImg;

namespace CRMSystem.Business.Abstractions;

public interface IAttachmentImgService
{
    Task<long> CreateAttachmentImg(long attachmentId, FileItem file, string? description, CancellationToken ct);
    Task<(Stream FileStream, string ContentType)> GetImageStream(long id, CancellationToken ct);
    Task<long> DeleteAttachmentImg(long id, CancellationToken ct);
    Task<int> GetCountAttachmentImg(AttachmentImgFilter filter, CancellationToken ct);
    Task<List<AttachmentImgItem>> GetPagedAttachmentImg(AttachmentImgFilter filter, CancellationToken ct);
    Task<long> UpdateAttachmentImg(long id, string? filePath, string? description, CancellationToken ct);
}