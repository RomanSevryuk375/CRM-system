using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.Tax;

public record TaxRequest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("rate")]
    public decimal Rate { get; init; }

    [JsonPropertyName("type")]
    public TaxTypeEnum TypeId { get; init; }
}