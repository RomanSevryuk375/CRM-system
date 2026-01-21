using System.Text.Json.Serialization;

namespace Shared.Contracts.StorageCell;

public record StorageCellRequest
{
    [JsonPropertyName("rack")]
    public string Rack { get; init; } = string.Empty;

    [JsonPropertyName("shelf")]
    public string Shelf { get; init; } = string.Empty;
};
