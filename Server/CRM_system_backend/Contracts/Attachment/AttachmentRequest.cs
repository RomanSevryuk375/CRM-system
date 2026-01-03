namespace CRM_system_backend.Contracts.Attachment;

public record AttachmentRequest
(
    long OrderId,
    string Worker,
    int WorkerId,
    DateTime CreateAt,
    string? Description
);
