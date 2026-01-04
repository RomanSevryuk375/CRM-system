namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseResponse
{
    public long Id { get; init; }
    public DateTime Date { get; init; }
    public string Category { get; init; } = string.Empty;
    public string? Tax { get; init; }
    public int? TaxId { get; init; }
    public long? PartSetId { get; init; }
    public string ExpenseType { get; init; } = string.Empty;
    public int ExpenceTypeId { get; init; }
    public decimal Sum { get; init; }
};
