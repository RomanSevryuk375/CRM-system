namespace CRM_system_backend.Contracts.Attachment;

public record AttachmentResponse
(
    long id,
    long orderId,
    string worker,
    int workerId,
    DateTime createAt,
    string? description
);
