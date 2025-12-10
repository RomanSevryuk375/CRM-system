using CRMSystem.Core.Enums;

namespace CRMSystem.DataAccess.Entites;

public class ExpenseEntity
{
    public long Id { get; set; }
    public int? TaxId { get; set; }
    public long? PartSetId { get; set; }
    public DateTime Date { get; set; }
    public string Category { get; set; } = string.Empty;
    public ExpenseTypeEnum ExpenseTypeId { get; set; } 
    public decimal Sum { get; set; }

    public TaxEntity? Tax { get; set; }
    public PartSetEntity? PartSet { get; set; }
    public ExpenseTypeEntity? ExpenseType { get; set; }
}
