namespace CRM_system_backend.Contracts.AttachmentImg;

public record AttachmentImgRequest
(
    long attachmentId,
    string filePath,
    string? description
);
