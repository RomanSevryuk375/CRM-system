using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.Core.Models;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Abstractions;

public interface IWorkProposalService
{
    Task<long> AcceptProposal(long id, CancellationToken ct);
    Task<long> CreateProposal(WorkProposal workProposal, CancellationToken ct);
    Task<long> DeleteProposal(long id, CancellationToken ct);
    Task<int> GetCountProposals(WorkProposalFilter filter, CancellationToken ct);
    Task<List<WorkProposalItem>> GetPagedProposals(WorkProposalFilter filter, CancellationToken ct);
    Task<WorkProposalItem> GetProposalById(long id, CancellationToken ct);
    Task<long> RejectProposal(long id, CancellationToken ct);
    Task<long> UpdateProposal(long id, ProposalStatusEnum? statusId, CancellationToken ct);
}