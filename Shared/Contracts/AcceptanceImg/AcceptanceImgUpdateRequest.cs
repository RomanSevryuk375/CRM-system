// Ignore Spelling: Img

using System.Text.Json.Serialization;

namespace Shared.Contracts.AcceptanceImg;

public record AcceptanceImgUpdateRequest
{
    [JsonPropertyName("filePath")]
    public string? FilePath { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }
};
