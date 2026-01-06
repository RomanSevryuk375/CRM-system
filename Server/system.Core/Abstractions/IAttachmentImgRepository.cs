// Ignore Spelling: Img

using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IAttachmentImgRepository
{
    Task<long> Create(AttachmentImg attachmentImg);
    Task<long> Delete(long id);
    Task<int> GetCount(AttachmentImgFilter filter);
    Task<List<AttachmentImgItem>> GetPaged(AttachmentImgFilter filter);
    Task<AttachmentImgItem?> GetById(long id);
    Task<long> Update(long id, string? filePath, string? description);
}