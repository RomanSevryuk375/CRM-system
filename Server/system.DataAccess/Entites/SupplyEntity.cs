namespace CRMSystem.DataAccess.Entites;

public class SupplyEntity
{
    public long Id { get; set; }
    public long SupplierId { get; set; }
    public DateOnly Date { get; set; }

    public SupplierEntity? Supplier { get; set; }
    public ICollection<SupplySetEntity> SupplySets { get; set; } = new HashSet<SupplySetEntity>();
}
