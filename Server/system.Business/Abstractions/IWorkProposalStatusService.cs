using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Business.Abstractions;

public interface IWorkProposalStatusService
{
    Task<List<WorkProposalStatusItem>> GetProposalStatuses(CancellationToken ct);
}