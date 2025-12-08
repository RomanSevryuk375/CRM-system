namespace CRMSystem.DataAccess.Entites;

public class SpecializationEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<SkillEntity> Skills { get; set; } = new HashSet<SkillEntity>();
}
