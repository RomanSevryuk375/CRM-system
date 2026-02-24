using Shared.Enums;

namespace CRMSystem.Core.ProjectionModels.WorkProposal;

public record WorkProposalCreateModel
(
    long OrderId,
    long JobId,
    int WorkerId,
    ProposalStatusEnum StatusId,
    DateTime Date);