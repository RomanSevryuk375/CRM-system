// Ignore Spelling: Img

using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.ProjectionModels.AttachmentImg;

namespace CRMSystem.Business.Abstractions;

public interface IAttachmentImgService
{
    Task<long> CreateAttachmentImg(long attachmentId, FileItem file, string? description);
    Task<(Stream FileStream, string ContentType)> GetImageStream(long id);
    Task<long> DeleteAttachmentImg(long id);
    Task<int> GetCountAttachmentImg(AttachmentImgFilter filter);
    Task<List<AttachmentImgItem>> GetPagedAttachmentImg(AttachmentImgFilter filter);
    Task<long> UpdateAttachmentImg(long id, string? filePath, string? description);
}