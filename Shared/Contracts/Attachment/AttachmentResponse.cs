using System.Text.Json.Serialization;

namespace Shared.Contracts.Attachment;

public record AttachmentResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("worker")]
    public string Worker { get; init; } = string.Empty;

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("createAt")]
    public DateTime CreateAt { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
};
