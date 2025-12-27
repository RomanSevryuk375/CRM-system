namespace CRMSystem.Core.DTOs.AttachmentImg;

public record AttachmentImgItem
(
    long id,
    long attachmentId,
    string filePath,
    string? description
);
