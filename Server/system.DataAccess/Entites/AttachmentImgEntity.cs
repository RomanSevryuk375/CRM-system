namespace CRMSystem.DataAccess.Entites;

public class AttachmentImgEntity
{
    public long Id { get; set; }
    public long AttachmentId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public AttachmentImgEntity? AttachmentImg { get; set; }
}
