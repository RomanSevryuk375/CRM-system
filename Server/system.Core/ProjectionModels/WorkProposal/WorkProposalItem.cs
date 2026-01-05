using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.WorkProposal;

public record WorkProposalItem
{
    public long Id { get; init; }
    public long OrderId { get; init; }
    public string Job { get; init; } = string.Empty;
    public long JobId { get; init; }
    public string Worker { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public string Status { get; init; } = string.Empty;
    public int StatusId { get; init; }
    public DateTime Date { get; init; }
};
