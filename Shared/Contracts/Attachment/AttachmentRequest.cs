namespace Shared.Contracts.Attachment;

public record AttachmentRequest
(
    long OrderId,
    string Worker,
    int WorkerId,
    DateTime CreateAt,
    string? Description
);
