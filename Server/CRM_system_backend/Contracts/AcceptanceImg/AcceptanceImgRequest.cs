// Ignore Spelling: Img

namespace CRM_system_backend.Contracts.AcceptanceImg;

public record CreateAcceptanceImgRequest
{
    public long AcceptanceId { get; set; }
    public required IFormFile File { get; set; }
    public string? Description { get; set; }
}
