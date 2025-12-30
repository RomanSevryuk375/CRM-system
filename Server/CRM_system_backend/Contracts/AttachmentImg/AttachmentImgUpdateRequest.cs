namespace CRM_system_backend.Contracts.AttachmentImg;

public record AttachmentImgUpdateRequest
(
    string? filePath,
    string? description
);
