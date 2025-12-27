using CRMSystem.Core.DTOs.Attachment;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Abstractions;

public interface IAttachmentService
{
    Task<long> CreateAttachment(Attachment attachment);
    Task<long> DeleteingAttachment(long id);
    Task<int> GetCountAttchment(AttachmentFilter filter);
    Task<List<AttachmentItem>> GetPagedAttachments(AttachmentFilter filter);
    Task<long> UpdateAttachment(long id, string? description);
}