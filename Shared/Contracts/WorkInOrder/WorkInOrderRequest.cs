using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.WorkInOrder;

public record WorkInOrderRequest
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("jobId")]
    public long JobId { get; init; }

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("statusId")]
    public WorkStatusEnum StatusId { get; init; }

    [JsonPropertyName("timeSpent")]
    public decimal TimeSpent { get; init; }
};
