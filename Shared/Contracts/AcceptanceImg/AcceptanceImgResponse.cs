// Ignore Spelling: Img

namespace Shared.Contracts.AcceptanceImg;

public record AcceptanceImgResponse
{
    public long Id { get; init; }
    public long AcceptanceId { get; init; }
    public string FilePath { get; init; } = string.Empty;
    public string? Description { get; init; }
};
