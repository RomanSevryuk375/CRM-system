using System.Text.Json.Serialization;

namespace Shared.Contracts.Position;

public record PositionRequest
{
    [JsonPropertyName("partId")]
    public long PartId { get; init; }

    [JsonPropertyName("cellId")]
    public int CellId { get; init; }

    [JsonPropertyName("purchasePrice")]
    public decimal PurchasePrice { get; init; }

    [JsonPropertyName("sellingPrice")]
    public decimal SellingPrice { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }
};
