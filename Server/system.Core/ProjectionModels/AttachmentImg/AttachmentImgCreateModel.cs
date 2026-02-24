namespace CRMSystem.Core.ProjectionModels.AttachmentImg;

public record AttachmentImgCreateModel
(
    long AttachmentId, 
    string FilePath, 
    string? Description);