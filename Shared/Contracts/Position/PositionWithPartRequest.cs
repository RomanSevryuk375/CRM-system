using System.Text.Json.Serialization;

namespace Shared.Contracts.Position;

public record PositionWithPartRequest
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

    [JsonPropertyName("categoryId")]
    public int CategoryId { get; init; }

    [JsonPropertyName("oenArticle")]
    public string? OemArticle { get; init; }

    [JsonPropertyName("manufacturerArticle")]
    public string? ManufacturerArticle { get; init; }

    [JsonPropertyName("internalArticle")]
    public string InternalArticle { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("manufacturer")]
    public string Manufacturer { get; init; } = string.Empty;

    [JsonPropertyName("applicability")]
    public string Applicability { get; init; } = string.Empty;
};