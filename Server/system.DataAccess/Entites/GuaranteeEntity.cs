namespace CRMSystem.DataAccess.Entites;

public class GuaranteeEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public string? Description { get; set; } = string.Empty;
    public string Terms { get; set; } = string.Empty;

    public OrderEntity? Order { get; set; }
}
