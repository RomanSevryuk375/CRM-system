namespace CRMSystem.DataAccess.Entites;

public class PartCategoryEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public ICollection<PartEntity> Parts { get; set; } = new List<PartEntity>();
}
