namespace CRM_system_backend.Contracts.AttachmentImg;

public record AttachmentImgRequest
{
    public long AttachmentId { get; set; }
    public required IFormFile File { get; set; }
    public string? Description { get; set; }
}
