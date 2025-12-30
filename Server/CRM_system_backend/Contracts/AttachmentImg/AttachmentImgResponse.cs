namespace CRM_system_backend.Contracts.AttachmentImg;

public record AttachmentImgResponse
(
    long id,
    long attachmentId,
    string filePath,
    string? description
);