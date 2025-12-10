namespace CRMSystem.DataAccess.Entites;

public class ExpenseTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<ExpenseEntity> Expenses { get; set; } = new HashSet<ExpenseEntity>();
}
