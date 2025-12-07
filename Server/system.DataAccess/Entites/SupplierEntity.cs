namespace CRMSystem.DataAccess.Entites;

public class SupplierEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Contacts { get; set; } = string.Empty;

    public ICollection<SupplyEntity> Supplies { get; set; } = new List<SupplyEntity>();
}
