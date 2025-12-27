namespace CRMSystem.DataAccess.Entites;

public class PartEntity
{
    public long Id { get; set; }
    public int CategoryId { get; set; }
    public string? OEMArticle { get; set; } = string.Empty;
    public string? ManufacturerArticle { get; set; } = string.Empty;
    public string InternalArticle { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string Name {  get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public string Applicability { get; set; } = string.Empty;

    public PartCategoryEntity? PartCategory { get; set; }
    public ICollection<PositionEntity> Positions { get; set; } = new HashSet<PositionEntity>();
}
