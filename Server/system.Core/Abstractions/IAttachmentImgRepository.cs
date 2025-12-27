using CRMSystem.Core.DTOs.AttachmentImg;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IAttachmentImgRepository
    {
        Task<long> Create(AttachmentImg attachmentImg);
        Task<long> Delete(long id);
        Task<int> GetCount(AttachmentImgFilter filter);
        Task<List<AttachmentImgItem>> GetPaged(AttachmentImgFilter filter);
        Task<long> Update(long id, string? filePath, string? description);
    }
}