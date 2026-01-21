using System.Text.Json.Serialization;

namespace Shared.Contracts.SupplySet;

public record SupplySetUpdateRequest
{
    [JsonPropertyName("quantity")]
    public decimal? Quantity { get; init; }

    [JsonPropertyName("purchasePrice")]
    public decimal? PurchasePrice { get; init; }
};
