using System.Text.Json.Serialization;

namespace Shared.Contracts.Notification;

public record NotificationResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("client")]
    public string Client { get; init; } = string.Empty;

    [JsonPropertyName("clientId")]
    public long ClientId { get; init; }

    [JsonPropertyName("car")]
    public string Car { get; init; } = string.Empty;

    [JsonPropertyName("carId")]
    public long CarId { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("typeId")]
    public int TypeId { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("statusId")]
    public int StatusId { get; init; }

    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("sendAt")]
    public DateTime SendAt { get; init; }
};
