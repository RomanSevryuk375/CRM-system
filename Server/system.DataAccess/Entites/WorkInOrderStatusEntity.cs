namespace CRMSystem.DataAccess.Entites;

public class WorkInOrderStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<WorkEntity> Works { get; set; } = new HashSet<WorkEntity>();
}
