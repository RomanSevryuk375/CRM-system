using CRMSystem.Core.DTOs.Attachment;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IAttachmentRepository
    {
        Task<long> Create(Attachment attachment);
        Task<long> Delete(long id);
        Task<int> GetCount(AttachmentFilter filter);
        Task<List<AttachmentItem>> GetPaged(AttachmentFilter filter);
        Task<long> Update(long id, string? description);
        Task<bool> Exists(long id);
    }
}