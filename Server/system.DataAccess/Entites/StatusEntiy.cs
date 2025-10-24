namespace CRMSystem.DataAccess.Entites;

public class StatusEntiy
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ICollection<BillEntity> Bills { get; set; } = new List<BillEntity>();

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
}
