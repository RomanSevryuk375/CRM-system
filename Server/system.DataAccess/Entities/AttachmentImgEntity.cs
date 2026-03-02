// Ignore Spelling: Img

namespace CRMSystem.DataAccess.Entities;

public class AttachmentImgEntity
{
    public long Id { get; set; }
    public long AttachmentId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public AttachmentEntity? Attachment { get; set; }
}
