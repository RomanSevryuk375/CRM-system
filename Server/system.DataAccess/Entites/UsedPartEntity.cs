namespace CRMSystem.DataAccess.Entites;

public class UsedPartEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int SupplierId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Article { get; set; } = string.Empty;

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Sum { get; set; }

    public OrderEntity? Order { get; set; }

    public SupplierEntity? Supplier { get; set; }

    public ICollection<ExpenseEntity> Expenses { get; set; } = new List<ExpenseEntity>();

    public ICollection<ProposedPartEntity> ProposedParts { get; set; } = new HashSet<ProposedPartEntity>();
}
