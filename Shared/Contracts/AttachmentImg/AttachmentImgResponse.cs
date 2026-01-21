// Ignore Spelling: Img

using System.Text.Json.Serialization;

namespace Shared.Contracts.AttachmentImg;

public record AttachmentImgResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("attachmentId")]
    public long AttachmentId { get; init; }

    [JsonPropertyName("filePath")]
    public string FilePath { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
};