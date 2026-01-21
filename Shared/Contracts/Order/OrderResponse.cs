using System.Text.Json.Serialization;

namespace Shared.Contracts.Order;

public record OrderResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("statusId")]
    public int StatusId { get; init; }

    [JsonPropertyName("car")]
    public string Car { get; init; } = string.Empty;

    [JsonPropertyName("carId")]
    public long CarId { get; init; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; init; }

    [JsonPropertyName("priority")]
    public string Priority { get; init; } = string.Empty;

    [JsonPropertyName("priorityId")]
    public int PriorityId { get; init; }
};
