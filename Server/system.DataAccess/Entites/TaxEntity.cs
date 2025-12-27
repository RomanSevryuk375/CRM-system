using CRMSystem.Core.Enums;

namespace CRMSystem.DataAccess.Entites;

public class TaxEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public int TypeId { get; set; }

    public TaxTypeEntity? TaxType { get; set; }
    public ICollection<ExpenseEntity> Expenses { get; set; } = new HashSet<ExpenseEntity>();

}
