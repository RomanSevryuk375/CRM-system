using CRMSystem.Core.ProjectionModels.WorkProposalStatus;

namespace CRMSystem.Business.Abstractions;

public interface IWorkProposalStatusService
{
    Task<List<WorkProposalStatusItem>> GetProposalStatuses(CancellationToken ct);
    Task<WorkProposalStatusItem> GetWorkProposalStatusById(int id, CancellationToken ct);
}