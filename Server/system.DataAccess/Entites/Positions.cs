namespace CRMSystem.DataAccess.Entites;

public class Positions
{
    public int Id { get; set; }

    public int PartId { get; set; }

    public int CellId { get; set; }

    public decimal PurchasePrice { get; set; } 

    public decimal SellingPrice { get; set; }
}
