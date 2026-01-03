namespace CRMSystem.Core.DTOs.AttachmentImg;

public record AttachmentImgResponse
(
    long Id,
    long AttachmentId,
    string FilePath,
    string? Description
);