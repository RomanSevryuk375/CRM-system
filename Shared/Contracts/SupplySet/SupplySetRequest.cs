using System.Text.Json.Serialization;

namespace Shared.Contracts.SupplySet;

public record SupplySetRequest
{
    [JsonPropertyName("supplierId")]
    public long SupplyId { get; init; }

    [JsonPropertyName("positionId")]
    public long PositionId { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("purchasePrice")]
    public decimal PurchasePrice { get; init; }
};