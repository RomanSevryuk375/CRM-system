using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IAttachmentRepository
{
    Task<long> Create(Attachment attachment);
    Task<long> Delete(long id);
    Task<int> GetCount(AttachmentFilter filter);
    Task<List<AttachmentItem>> GetPaged(AttachmentFilter filter);
    Task<long> Update(long id, string? description);
    Task<bool> Exists(long id);
}