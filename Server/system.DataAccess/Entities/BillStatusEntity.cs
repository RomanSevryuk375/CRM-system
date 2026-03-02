namespace CRMSystem.DataAccess.Entities;

public class BillStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<BillEntity> Bills { get; set; } = new HashSet<BillEntity>();
}
