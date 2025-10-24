namespace CRMSystem.DataAccess.Entites;

public class TaxEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Rate { get; set; }

    public string Type { get; set; } = string.Empty;

    public ICollection<ExpenseEntity> Expenses { get; set; } = new List<ExpenseEntity>();

}
