using System.Text.Json.Serialization;

namespace Shared.Contracts.Specialization;

public record SpecializationResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
};
