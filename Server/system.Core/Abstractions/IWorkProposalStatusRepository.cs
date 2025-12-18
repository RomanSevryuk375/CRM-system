using CRMSystem.Core.DTOs;

namespace CRMSystem.DataAccess.Repositories
{
    public interface IWorkProposalStatusRepository
    {
        Task<List<WorkProposalStatusItem>> Get();
    }
}