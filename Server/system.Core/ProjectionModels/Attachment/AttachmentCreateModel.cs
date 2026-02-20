namespace CRMSystem.Core.ProjectionModels.Attachment;

public record AttachmentCreateModel
(
    long OrderId,
    int WorkerId,
    DateTime CreateAt,
    string? Description);