using System.Text.Json.Serialization;

namespace Shared.Contracts.PartSet;

public record PartSetRequest
{
    [JsonPropertyName("orderId")]
    public long? OrderId { get; init; }

    [JsonPropertyName("positionId")]
    public long PositionId { get; init; }

    [JsonPropertyName("proposalId")]
    public long? ProposalId { get; init; }

    [JsonPropertyName("quantity")]
    public decimal Quantity { get; init; }

    [JsonPropertyName("soldPrice")]
    public decimal SoldPrice { get; init; }
};
