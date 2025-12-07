namespace CRMSystem.DataAccess.Entites;

public class SupplySetEntity
{
    public long Id { get; set; }
    public long SupplyId { get; set; }
    public long PositionId { get; set; }
    public decimal Quantity { get; set; }
    public decimal PurchasePrice { get; set; }

    public SupplyEntity? Supply { get; set; }
    public PositionEntity? Position { get; set; }
}
