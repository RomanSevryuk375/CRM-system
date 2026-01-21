using System.Text.Json.Serialization;

namespace Shared.Contracts.Bill;

public record BillResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("statusId")]
    public int StatusId { get; init; }

    [JsonPropertyName("createAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("actualBillDate")]
    public DateOnly? ActualBillDate { get; init; }
};
