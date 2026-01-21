using System.Text.Json.Serialization;

namespace Shared.Contracts.Specialization;

public record SpecializationRequest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
};
