namespace CRMSystem.DataAccess.Entites;

public class ExpenseEntity
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public string Category { get; set; } = string.Empty;

    public int? TaxId { get; set; }

    public int? UsedPartId { get; set; }

    public string ExpenseType { get; set; } = string.Empty;

    public decimal Sum { get; set; }

    public TaxEntity? Tax { get; set; }

    public UsedPartEntity? UsedPart { get; set; }
}
