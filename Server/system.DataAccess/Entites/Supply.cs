namespace CRMSystem.DataAccess.Entites;

public class Supply
{
    public int Id { get; set; }

    public int SupplierId { get; set; }

    public int PositionId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime Date { get; set; }
}
