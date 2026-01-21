using System.Text.Json.Serialization;

namespace Shared.Contracts.Supply;

public record SupplyResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("supplier")]
    public string Supplier { get; init; } = string.Empty;

    [JsonPropertyName("supplierId")]
    public int SupplierId { get; init; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; init; }
};
