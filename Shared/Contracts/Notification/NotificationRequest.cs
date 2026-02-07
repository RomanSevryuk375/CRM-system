using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Notification;

public record NotificationRequest
{
    [JsonPropertyName("clientId")]
    public long ClientId { get; init; }

    [JsonPropertyName("carId")]
    public long CarId { get; init; }

    [JsonPropertyName("typeId")]
    public NotificationTypeEnum TypeId { get; init; }

    [JsonPropertyName("statusId")]
    public NotificationStatusEnum StatusId { get; init; }

    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    [JsonPropertyName("sendAt")]
    public DateTime SendAt { get; init; }
};
