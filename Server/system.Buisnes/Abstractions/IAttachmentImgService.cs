using CRMSystem.Core.DTOs.AttachmentImg;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAttachmentImgService
{
    Task<long> CreateAttachmentImg(AttachmentImg attachmentImg);
    Task<long> DeleteAttachmentImg(long id);
    Task<int> GetCountAttachmentImg(AttachmentImgFilter filter);
    Task<List<AttachmentImgItem>> GetPagedAttachmentImg(AttachmentImgFilter filter);
    Task<long> UpdateAttaachmentImg(long id, string? filePath, string? description);
}