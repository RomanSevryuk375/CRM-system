// Ignore Spelling: Img

namespace Shared.Contracts.AcceptanceImg;

public record CreateAcceptanceImgRequest
{
    public long AcceptanceId { get; set; }
    public required IFormFile File { get; set; }
    public string? Description { get; set; }
}
