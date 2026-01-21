using System.Text.Json.Serialization;

namespace Shared.Contracts.Work;

public record WorkResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("category")]
    public string Category { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("standardTime")]
    public decimal StandardTime { get; init; }
};
