namespace CRMSystem.Core.DTOs.AttachmentImg;

public record AttachmentImgItem
(
    long Id,
    long AttachmentId,
    string FilePath,
    string? Description
);
