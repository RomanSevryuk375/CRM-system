namespace CRMSystem.DataAccess.Entites;

public class TaxTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<TaxEntity> Taxes { get; set; } = new HashSet<TaxEntity>();
}
