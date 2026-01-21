using System.Text.Json.Serialization;

namespace Shared.Contracts.PartSet;

public record PartSetResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    long? OrderId { get; init; }

    [JsonPropertyName("positionId")]
    long PositionId { get; init; }

    [JsonPropertyName("proposalId")]
    long? ProposalId { get; init; }

    [JsonPropertyName("quantity")]
    decimal Quantity { get; init; }

    [JsonPropertyName("soldPrice")]
    decimal SoldPrice { get; init; }
};
