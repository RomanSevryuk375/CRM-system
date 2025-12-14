namespace CRMSystem.Core.DTOs.AttachmentImg;

public record AttachmentImgFilter
(
    IEnumerable<long> attachmentIds,
    int Page,
    int Limit
);
