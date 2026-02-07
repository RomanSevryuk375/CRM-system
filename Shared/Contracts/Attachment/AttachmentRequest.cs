using System.Text.Json.Serialization;

namespace Shared.Contracts.Attachment;

public record AttachmentRequest
{
    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("createAt")]
    public DateTime CreateAt { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
};
