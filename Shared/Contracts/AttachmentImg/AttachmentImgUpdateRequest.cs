// Ignore Spelling: Img

using System.Text.Json.Serialization;

namespace Shared.Contracts.AttachmentImg;

public record AttachmentImgUpdateRequest
{
    [JsonPropertyName("filePath")]
    public string? FilePath { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
};
