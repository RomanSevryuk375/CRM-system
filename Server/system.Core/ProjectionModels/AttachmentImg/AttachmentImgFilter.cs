namespace CRMSystem.Core.DTOs.AttachmentImg;

public record AttachmentImgFilter
(
    IEnumerable<long> AttachmentIds,
    int Page,
    int Limit
);
