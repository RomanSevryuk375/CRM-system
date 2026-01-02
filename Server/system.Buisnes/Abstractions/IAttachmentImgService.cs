using CRMSystem.Core.DTOs;
using CRMSystem.Core.DTOs.AttachmentImg;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAttachmentImgService
{
    Task<long> CreateAttachmentImg(long attachmentId, FileItem file, string? description);
    Task<(Stream FileStream, string ContentType)> GetImageStream(long id);
    Task<long> DeleteAttachmentImg(long id);
    Task<int> GetCountAttachmentImg(AttachmentImgFilter filter);
    Task<List<AttachmentImgItem>> GetPagedAttachmentImg(AttachmentImgFilter filter);
    Task<long> UpdateAttaachmentImg(long id, string? filePath, string? description);
}