using System.Text.Json.Serialization;

namespace Shared.Contracts.Guarantee;

public record GuaranteeUpdateRequest
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("terms")]
    public string? Terms { get; init; }
};
