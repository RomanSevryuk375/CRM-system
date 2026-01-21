using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Order;

public record OrderUpdateRequest
{
    [JsonPropertyName("priorityId")]
    public OrderPriorityEnum? PriorityId { get; init; }
};
