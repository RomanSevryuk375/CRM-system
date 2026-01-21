using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Order;

public record OrderWithBillRequest
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderStatusId")]
    public OrderStatusEnum OrderStatusId { get; init; }

    [JsonPropertyName("carId")]
    public long CarId { get; init; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; init; }

    [JsonPropertyName("priorityId")]
    public OrderPriorityEnum PriorityId { get; init; }

    [JsonPropertyName("ownerId")]
    public long OrderId { get; init; }

    [JsonPropertyName("billStatusId")]
    public BillStatusEnum BillStatusId { get; init; }

    [JsonPropertyName("createAt")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("actualBillDate")]
    public DateOnly? ActualBillDate { get; init; }
};
