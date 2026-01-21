using System.Text.Json.Serialization;

namespace Shared.Contracts.Expense;

public record ExpenseResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("date")]
    public DateTime Date { get; init; }

    [JsonPropertyName("category")]
    public string Category { get; init; } = string.Empty;

    [JsonPropertyName("tax")]
    public string? Tax { get; init; }

    [JsonPropertyName("taxId")]
    public int? TaxId { get; init; }

    [JsonPropertyName("partSetId")]
    public long? PartSetId { get; init; }

    [JsonPropertyName("expenseType")]
    public string ExpenseType { get; init; } = string.Empty;

    [JsonPropertyName("expenseTypeId")]
    public int ExpenseTypeId { get; init; }

    [JsonPropertyName("sum")]
    public decimal Sum { get; init; }
};
