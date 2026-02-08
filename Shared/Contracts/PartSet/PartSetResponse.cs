using System.Text.Json.Serialization;

namespace Shared.Contracts.PartSet;

public record PartSetResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long? OrderId { get; init; }

    [JsonPropertyName("position")]
    public string Position { get; init; } = string.Empty;

    [JsonPropertyName("positionId")]
    public long PositionId { get; init; }

    [JsonPropertyName("proposalId")]
    public long? ProposalId { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("soldPrice")]
    public decimal SoldPrice { get; init; }
};
