namespace CRMSystem.DataAccess.Entites;

public class GuaranteesEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime DateEnd { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Terms { get; set; } = string.Empty;

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>(); 
}
