namespace CRMSystem.DataAccess.Entites;

public class OrderPriorityEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();
}
