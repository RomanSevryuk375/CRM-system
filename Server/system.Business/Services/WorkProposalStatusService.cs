using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.WorkProposalStatus;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class WorkProposalStatusService(
    IWorkProposalStatusRepository workProposalStatusRepository,
    ILogger<WorkProposalStatusService> logger) : IWorkProposalStatusService
{
    public async Task<WorkProposalStatusItem> GetWorkProposalStatusById(int id, CancellationToken ct)
    {
        return await workProposalStatusRepository.GetById(id, ct)
               ?? throw new NotFoundException($"WorkInOrder {id} not found");
    }
    
    public async Task<List<WorkProposalStatusItem>> GetProposalStatuses(CancellationToken ct)
    {
        logger.LogInformation("Getting proposal statuses start");

        var statuses = await workProposalStatusRepository.Get(ct);

        logger.LogInformation("Getting proposal statuses success");

        return statuses;
    }
}
