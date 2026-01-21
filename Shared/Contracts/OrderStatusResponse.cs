using System.Text.Json.Serialization;

namespace Shared.Contracts;

public record OrderStatusResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
};
