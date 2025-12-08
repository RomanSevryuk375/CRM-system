namespace CRMSystem.DataAccess.Entites;

public class AcceptanceImgEntity
{
    public long Id { get; set; }
    public long AcceptanceId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public AcceptanceEntity? Acceptance { get; set; }

}
