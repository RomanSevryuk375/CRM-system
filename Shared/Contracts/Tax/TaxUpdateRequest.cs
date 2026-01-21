using System.Text.Json.Serialization;

namespace Shared.Contracts.Tax;

public record TaxUpdateRequest
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("rate")]
    public decimal? Rate { get; init; }
};
