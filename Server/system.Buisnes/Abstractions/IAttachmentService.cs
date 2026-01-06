using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IAttachmentService
{
    Task<long> CreateAttachment(Attachment attachment);
    Task<long> DeletingAttachment(long id);
    Task<int> GetCountAttachment(AttachmentFilter filter);
    Task<List<AttachmentItem>> GetPagedAttachments(AttachmentFilter filter);
    Task<long> UpdateAttachment(long id, string? description);
}