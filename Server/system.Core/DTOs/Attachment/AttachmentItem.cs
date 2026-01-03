namespace CRMSystem.Core.DTOs.Attachment;

public record AttachmentItem
(
    long Id,
    long OrderId,
    string Worker,
    int WorkerId,
    DateTime CreateAt,
    string? Description
);

