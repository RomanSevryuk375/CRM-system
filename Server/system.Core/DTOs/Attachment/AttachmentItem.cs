namespace CRMSystem.Core.DTOs.Attachment;

public record AttachmentItem
(
    long id,
    long orderId,
    string worker,
    int workerId,
    DateTime createAt,
    string? description
);

