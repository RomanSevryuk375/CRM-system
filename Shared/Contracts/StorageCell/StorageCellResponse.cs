using System.Text.Json.Serialization;

namespace Shared.Contracts.StorageCell;

public record StorageCellResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("rack")]
    public string Rack { get; init; } = string.Empty;

    [JsonPropertyName("shelf")]
    public string Shelf { get; init; } = string.Empty;
};
