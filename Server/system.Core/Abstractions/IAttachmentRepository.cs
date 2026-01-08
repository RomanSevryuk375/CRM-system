using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IAttachmentRepository
{
    Task<long> Create(Attachment attachment, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(AttachmentFilter filter, CancellationToken ct);
    Task<List<AttachmentItem>> GetPaged(AttachmentFilter filter, CancellationToken ct);
    Task<long> Update(long id, string? description, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct);
}