namespace CRM_system_backend.Contracts.Attachment;

public record AttachmentRequest
(
    long orderId,
    string worker,
    int workerId,
    DateTime createAt,
    string? description
);
