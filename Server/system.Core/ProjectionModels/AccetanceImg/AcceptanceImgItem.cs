// Ignore Spelling: Img

namespace CRMSystem.Core.ProjectionModels.AccetanceImg;

public record AcceptanceImgItem
{
    public long Id { get; init; }
    public long AcceptanceId { get; init; }
    public string FilePath { get; init; } = string.Empty;
    public string? Description { get; init; }
};
