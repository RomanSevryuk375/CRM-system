// Ignore Spelling: Oem

using System.Text.Json.Serialization;

namespace Shared.Contracts.Part;

public record PartResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("categoryId")]
    public int CategoryId { get; init; }

    [JsonPropertyName("oemArtiocle")]
    public string? OemArticle { get; init; }

    [JsonPropertyName("manufacturerArticle")]
    public string? ManufacturerArticle { get; init; }

    [JsonPropertyName("internalArticle")]
    public string InternalArticle { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; init; } = string.Empty;

    [JsonPropertyName("applicability")]
    public string Applicability { get; init; } = string.Empty;
};
