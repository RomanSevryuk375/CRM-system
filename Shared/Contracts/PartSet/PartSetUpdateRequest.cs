using System.Text.Json.Serialization;

namespace Shared.Contracts.PartSet;

public record PartSetUpdateRequest
{
    [JsonPropertyName("quantity")]
    public decimal? Quantity { get; init; }

    [JsonPropertyName("soldPrice")]
    public decimal? SoldPrice { get; init; }
};
