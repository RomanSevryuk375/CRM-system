using Shared.Enums;
using System.Text.Json.Serialization;

namespace Shared.Contracts.WorkPropossal;

public record WorkProposalRequest
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("orderId")]
    public long OrderId { get; init; }

    [JsonPropertyName("jobId")]
    public long JobId { get; init; }

    [JsonPropertyName("workerId")]
    public int WorkerId { get; init; }

    [JsonPropertyName("statusId")]
    public ProposalStatusEnum StatusId { get; init; }

    [JsonPropertyName("date")]
    public DateTime Date { get; init; }
};
