namespace CRMSystem.Core.DTOs.Attachment;

public record AttachmentFilter
(
    IEnumerable<long>? attachmentIds,
    IEnumerable<int>? WorkerIds,
    IEnumerable<long>? OrderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
