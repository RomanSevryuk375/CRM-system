namespace CRMSystem.DataAccess.Entities;

public class OrderStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();
}
