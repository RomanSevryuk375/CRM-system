namespace CRMSystem.DataAccess.Entites;

public class ExpenseEntity
{
    public long Id { get; set; }
    public int? TaxId { get; set; }
    public long? PartSetId { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public string ExpenseType { get; set; } = string.Empty;
    public decimal Sum { get; set; }

    public TaxEntity? Tax { get; set; }
    public PartSetEntity? PartSet { get; set; }
}
