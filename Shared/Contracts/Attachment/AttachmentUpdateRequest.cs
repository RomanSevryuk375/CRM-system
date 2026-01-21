using System.Text.Json.Serialization;

namespace Shared.Contracts.Attachment;

public record AttachmentUpdateRequest
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }
};
