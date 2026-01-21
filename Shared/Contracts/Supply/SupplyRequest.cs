using System.Text.Json.Serialization;

namespace Shared.Contracts.Supply;

public record SupplyRequest
{
    [JsonPropertyName("supplierId")]
    public int SupplierId { get; init; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; init; }
}
