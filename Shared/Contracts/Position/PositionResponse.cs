using System.Text.Json.Serialization;

namespace Shared.Contracts.Position;

public record PositionResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("part")]
    public string Part { get; init; } = string.Empty;

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
