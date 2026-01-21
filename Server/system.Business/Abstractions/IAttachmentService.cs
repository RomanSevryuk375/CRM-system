using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IAttachmentService
{
    Task<long> CreateAttachment(Attachment attachment, CancellationToken ct);
    Task<long> DeletingAttachment(long id, CancellationToken ct);
    Task<int> GetCountAttachment(AttachmentFilter filter, CancellationToken ct);
    Task<List<AttachmentItem>> GetPagedAttachments(AttachmentFilter filter, CancellationToken ct);
    Task<long> UpdateAttachment(long id, string? description, CancellationToken ct);
}