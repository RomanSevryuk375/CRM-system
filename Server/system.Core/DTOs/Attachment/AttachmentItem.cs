namespace CRMSystem.Core.DTOs.Attachment;

public record AttachmentItem
(
    long id,
    long orderId,
    string worker,
    DateTime createAt,
    string? description
);

