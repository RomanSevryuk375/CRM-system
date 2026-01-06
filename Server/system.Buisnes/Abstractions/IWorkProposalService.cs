using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;

namespace CRMSystem.Business.Abstractions;

public interface IWorkProposalService
{
    Task<long> AcceptProposal(long id);
    Task<long> CreateProposal(WorkProposal workProposal);
    Task<long> DeleteProposal(long id);
    Task<int> GetCountProposals(WorkProposalFilter filter);
    Task<List<WorkProposalItem>> GetPagedProposals(WorkProposalFilter filter);
    Task<WorkProposalItem> GetProposalById(long id);
    Task<long> RejectProposal(long id);
    Task<long> UpdateProposal(long id, ProposalStatusEnum? statusId);
}