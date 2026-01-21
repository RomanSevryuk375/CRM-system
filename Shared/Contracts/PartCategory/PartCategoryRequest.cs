using System.Text.Json.Serialization;

namespace Shared.Contracts.PartCategory;

public record PartCategoryRequest
{
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }
};
