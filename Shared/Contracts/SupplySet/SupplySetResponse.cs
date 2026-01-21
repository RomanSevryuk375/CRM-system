using System.Text.Json.Serialization;

namespace Shared.Contracts.SupplySet;

public record SupplySetResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("supplierId")]
    public long SupplyId { get; init; }

    [JsonPropertyName("position")]
    public string Position { get; init; } = string.Empty;

    [JsonPropertyName("positionId")]
    public long PositionId { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("purchasePrice")]
    public decimal PurchasePrice { get; init; }
};
