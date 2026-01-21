using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Expense;

public record ExpenseUpdateRequest
{
    [JsonPropertyName("date")]
    public DateTime? Date { get; init; }

    [JsonPropertyName("category")]
    public string? Category { get; init; }

    [JsonPropertyName("expenseTypeId")]
    public ExpenseTypeEnum? ExpenseTypeId { get; init; }

    [JsonPropertyName("sum")]
    public decimal? Sum { get; init; }
};
