// Ignore Spelling: Img

using CRMSystem.Core.ProjectionModels.AttachmentImg;
using CRMSystem.Core.Models;
using Shared.Filters;

namespace CRMSystem.Core.Abstractions;

public interface IAttachmentImgRepository
{
    Task<long> Create(AttachmentImg attachmentImg, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<int> GetCount(AttachmentImgFilter filter, CancellationToken ct);
    Task<List<AttachmentImgItem>> GetPaged(AttachmentImgFilter filter, CancellationToken ct);
    Task<AttachmentImgItem?> GetById(long id, CancellationToken ct);
    Task<long> Update(long id, string? filePath, string? description, CancellationToken ct);
}