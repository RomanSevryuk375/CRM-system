using CRMSystem.Buisnes.DTOs;
using CRMSystem.Core.Models;

namespace CRMSystem.Buisnes.Services
{
    public interface IWorkPropossalService
    {
        Task<List<WorkProposal>> GetPagedWorkProposal(int page, int limit);
        Task<int> GetCountProposal();
        Task<List<WorkProposalWithInfoDto>> GetPagedWorkProposalWithInfo(int page, int limit);
        Task<List<WorkProposalWithInfoDto>> GetPagedProposalsForCar(List<int> carIds);
        Task<List<WorkProposalWithInfoDto>> GetPagedProposalsForCar(int carIds);
        Task<List<WorkProposalWithInfoDto>> GetPagedProposalForClient(int userId, int page, int limit);
        Task<int> GetCountProposalForClient(int userId);
        Task<int> CreateWorkProposal(WorkProposal workProposal);
        Task<int> DeleteWorkProposal(int id);
        Task<int> UpdateWorkProposal(int id, int? orderId, int? workId, int? byWorker, int? statusId, int? decisionStatusId, DateTime? date);
        Task<int> AcceptProposal(int id);
        Task<int> RejectProposal(int id);
    }
}