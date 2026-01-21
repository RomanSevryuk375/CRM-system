using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.WorkProposal;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Core.Abstractions;

public interface IWorkProposalRepository
{
    Task<long> AcceptProposal(long id, CancellationToken ct);
    Task<long> Create(WorkProposal workProposal, CancellationToken ct);
    Task<long> Delete(long id, CancellationToken ct);
    Task<List<WorkProposalItem>> GetPaged(WorkProposalFilter filter, CancellationToken ct);
    Task<int> GetCount(WorkProposalFilter filter, CancellationToken ct);
    Task<long> RejectProposal(long id, CancellationToken ct);
    Task<long> Update(long id, ProposalStatusEnum? statusId, CancellationToken ct);
    Task<WorkProposalItem?> GetById(long id, CancellationToken ct);
    Task<bool> Exists(long id, CancellationToken ct );
}