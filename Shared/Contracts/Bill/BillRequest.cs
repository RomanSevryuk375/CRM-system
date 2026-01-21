using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Bill;

public record BillRequest
{
    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("statusId")]
    public BillStatusEnum StatusId { get; init; }

    [JsonPropertyName("createAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("actualBillDate")]
    public DateOnly? ActualBillDate { get; init; }
};
