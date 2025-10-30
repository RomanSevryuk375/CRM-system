using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkPropossalRepository
    {
        Task<int> Create(WorkProposal workProposal);
        Task<int> Delete(int id);
        Task<List<WorkProposal>> Get();
        Task<int> Update(int id, int? orderId, int? workId, int? byWorker, int? statusId, int? decisionStatusId, DateTime? date);
    }
}