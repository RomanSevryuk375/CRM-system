using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Expense;

public record ExpenseRequest
{
    [JsonPropertyName("date")]
    public DateTime Date { get; init; }

    [JsonPropertyName("category")]
    public string Category { get; init; } = string.Empty;

    [JsonPropertyName("taxId")]
    public int? TaxId { get; init; }

    [JsonPropertyName("partSetId")]
    public long? PartSetId { get; init; }

    [JsonPropertyName("expenseTypeId")]
    public ExpenseTypeEnum ExpenseTypeId { get; init; }

    [JsonPropertyName("sum")]
    public decimal Sum { get; init; }
};
