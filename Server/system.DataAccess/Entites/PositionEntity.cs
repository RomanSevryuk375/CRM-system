namespace CRMSystem.DataAccess.Entites;

public class PositionEntity
{
    public long Id { get; set; }
    public long PartId { get; set; }
    public int CellId { get; set; }
    public decimal PurchasePrice { get; set; } 
    public decimal SellingPrice { get; set; }
    public int Quantity { get; set; }

    public StorageCellEntity? StorageCell { get; set; }
    public PartEntity? Part { get; set; }
    public ICollection<SupplyEntity> Supplies { get; set; } = new HashSet<SupplyEntity>();
    public ICollection<PartSetEntity> partSets { get; set; } = new List<PartSetEntity>();
}
