// Ignore Spelling: Img

namespace CRMSystem.DataAccess.Entities;

public class AcceptanceImgEntity
{
    public long Id { get; set; }
    public long AcceptanceId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public AcceptanceEntity? Acceptance { get; set; }

}
