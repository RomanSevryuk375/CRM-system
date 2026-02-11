using Shared.Enums;

namespace Shared.Contracts.WorkProposal;

public record ProposalStatusRequest
{
    public ProposalStatusEnum Status { get; init; }
}