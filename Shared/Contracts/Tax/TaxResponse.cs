using System.Text.Json.Serialization;

namespace Shared.Contracts.Tax;

public record TaxResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("rate")]
    public decimal Rate { get; init; }

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;
};
