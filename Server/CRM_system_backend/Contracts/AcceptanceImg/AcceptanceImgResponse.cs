// Ignore Spelling: Img

namespace CRM_system_backend.Contracts.AcceptanceImg;

public record AcceptanceImgResponse
{
    public long Id { get; init; }
    public long AcceptanceId { get; init; }
    public string FilePath { get; init; } = string.Empty;
    public string? Description { get; init; }
};
