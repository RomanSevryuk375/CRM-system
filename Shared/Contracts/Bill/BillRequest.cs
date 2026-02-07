using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Bill;

public record BillRequest
{
    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("statusId")]
    public BillStatusEnum StatusId { get; init; }

    [JsonPropertyName("createAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("actualBillDate")]
    public DateOnly? ActualBillDate { get; init; }
};
