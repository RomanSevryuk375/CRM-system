using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Order;

public record OrderWithBillRequest
{

    [JsonPropertyName("orderStatusId")]
    public OrderStatusEnum OrderStatusId { get; init; }

    [JsonPropertyName("carId")]
    public long CarId { get; init; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; init; }

    [JsonPropertyName("priorityId")]
    public OrderPriorityEnum PriorityId { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("billStatusId")]
    public BillStatusEnum BillStatusId { get; init; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("actualBillDate")]
    public DateOnly? ActualBillDate { get; init; }
};
