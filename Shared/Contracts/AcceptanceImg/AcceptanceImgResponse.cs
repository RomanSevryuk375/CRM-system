// Ignore Spelling: Img

using System.Text.Json.Serialization;

namespace Shared.Contracts.AcceptanceImg;

public record AcceptanceImgResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("acceptanceId")]
    public long AcceptanceId { get; init; }

    [JsonPropertyName("filePath")]
    public string FilePath { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
};
