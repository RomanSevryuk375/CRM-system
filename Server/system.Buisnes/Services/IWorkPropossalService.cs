using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkPropossalService
    {
        Task<int> CreateWorkProposal(WorkProposal workProposal);
        Task<int> DeleteWorkProposal(int id);
        Task<List<WorkProposal>> GetWorkProposal();
        Task<List<WorkProposalWithInfoDto>> GetWorkProposalWithInfo();
        Task<List<WorkProposalWithInfoDto>> GetProposalForClient(int userId);
        Task<int> UpdateWorkProposal(int id, int orderId, int workId, int byWorker, int statusId, int decisionStatusId, DateTime date);
        Task<int> AcceptProposal(int id);
        Task<int> RejectProposal(int id);
    }
}