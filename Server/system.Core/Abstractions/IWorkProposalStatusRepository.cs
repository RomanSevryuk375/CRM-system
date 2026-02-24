using CRMSystem.Core.ProjectionModels.WorkProposalStatus;

namespace CRMSystem.Core.Abstractions;

public interface IWorkProposalStatusRepository
{
    Task<List<WorkProposalStatusItem>> Get(CancellationToken ct);
    Task<WorkProposalStatusItem?> GetById(int id, CancellationToken ct);
    Task<bool> Exists(int id, CancellationToken ct);
}