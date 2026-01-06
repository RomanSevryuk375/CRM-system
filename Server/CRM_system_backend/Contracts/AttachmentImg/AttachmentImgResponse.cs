// Ignore Spelling: Img

namespace CRM_system_backend.Contracts.AttachmentImg;

public record AttachmentImgResponse
{
    public long Id { get; init; }
    public long AttachmentId { get; init; }
    public string FilePath { get; init; } = string.Empty;
    public string? Description { get; init; }
};