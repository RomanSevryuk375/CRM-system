using CRMSystem.Core.ProjectionModels;

namespace CRMSystem.Core.Abstractions;

public interface IWorkProposalStatusRepository
{
    Task<List<WorkProposalStatusItem>> Get();
    Task<bool> Exists(int id);
}