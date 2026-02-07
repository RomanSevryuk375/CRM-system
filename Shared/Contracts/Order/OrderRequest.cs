using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Order;

public record OrderRequest
{

    [JsonPropertyName("statusId")]
    public OrderStatusEnum StatusId { get; init; }

    [JsonPropertyName("carId")]
    public long CarId { get; init; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; init; }

    [JsonPropertyName("priorityId")]
    public OrderPriorityEnum PriorityId { get; init; }
};
