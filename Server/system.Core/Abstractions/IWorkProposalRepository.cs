using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.Core.Abstractions;

public interface IWorkProposalRepository
{
    Task<long> AcceptProposal(long id);
    Task<long> Create(WorkProposal workProposal);
    Task<long> Delete(long id);
    Task<List<WorkProposalItem>> GetPaged(WorkProposalFilter filter);
    Task<int> GetCount(WorkProposalFilter filter);
    Task<long> RejectProposal(long id);
    Task<long> Update(long id, ProposalStatusEnum? statusId);
    Task<WorkProposalItem?> GetById(long id);
    Task<bool> Exists(long id);
}