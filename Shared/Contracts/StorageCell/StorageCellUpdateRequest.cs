using System.Text.Json.Serialization;

namespace Shared.Contracts.StorageCell;

public record StorageCellUpdateRequest
{
    [JsonPropertyName("rack")]
    public string? Rack { get; init; } = string.Empty;

    [JsonPropertyName("shelf")]
    public string? Shelf { get; init; } = string.Empty;
};