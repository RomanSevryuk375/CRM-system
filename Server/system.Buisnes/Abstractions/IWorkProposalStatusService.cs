using CRMSystem.Core.DTOs;

namespace CRMSystem.Buisnes.Abstractions;

public interface IWorkProposalStatusService
{
    Task<List<WorkProposalStatusItem>> GetProposalStatuses();
}