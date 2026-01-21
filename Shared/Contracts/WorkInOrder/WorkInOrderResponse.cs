using System.Text.Json.Serialization;

namespace Shared.Contracts.WorkInOrder;

public record WorkInOrderResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("job")]
    public string Job { get; init; } = string.Empty;

    [JsonPropertyName("jobId")]
    public long JobId { get; init; }

    [JsonPropertyName("worker")]
    public string Worker { get; init; } = string.Empty;

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("statusId")]
    public int StatusId { get; init; }

    [JsonPropertyName("timeSpent")]
    public decimal TimeSpent { get; init; }
};
