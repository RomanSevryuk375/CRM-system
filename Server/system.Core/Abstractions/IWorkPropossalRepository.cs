using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkPropossalRepository
    {
        Task<List<WorkProposal>> Get();
        Task<List<WorkProposal>> GetPaged(int page, int limit);
        Task<int> GetCount();
        Task<List<WorkProposal>> GetByOrderId(List<int> orderIds);
        Task<List<WorkProposal>> GetPagedByOrderId(List<int> orderIds, int page, int limit);
        Task<int> GetCountByOrderId(List<int> orderIds);
        Task<int> Create(WorkProposal workProposal);
        Task<int> Delete(int id);
        Task<int> Update(int id, int? orderId, int? workId, int? byWorker, int? statusId, int? decisionStatusId, DateTime? date);
        Task<int> AcceptProposal(int id);
        Task<int> RejectProposal(int id);
    }
}