namespace CRMSystem.Core.DTOs.Attachment;

public record AttachmentFilter
(
    IEnumerable<long>? AttachmentIds,
    IEnumerable<int>? WorkerIds,
    IEnumerable<long>? OrderIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
