namespace CRMSystem.DataAccess.Entites;

public class SupplyEntity
{
    public long Id { get; set; }
    public long SupplierId { get; set; }
    public DateTime Date { get; set; }

    public SupplierEntity? Supplier { get; set; }
    public ICollection<SupplySetEntity> SupplySets { get; set; } = new List<SupplySetEntity>();
}
