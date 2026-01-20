// Ignore Spelling: Img

namespace Shared.Contracts.AttachmentImg;

public record AttachmentImgRequest
{
    public long AttachmentId { get; set; }
    public required IFormFile File { get; set; }
    public string? Description { get; set; }
}
