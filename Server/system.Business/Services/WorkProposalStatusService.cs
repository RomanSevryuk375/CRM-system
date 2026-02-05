using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class WorkProposalStatusService(
    IWorkProposalStatusRepository workProposalStatusRepository,
    ILogger<WorkProposalStatusService> logger) : IWorkProposalStatusService
{
    public async Task<List<WorkProposalStatusItem>> GetProposalStatuses(CancellationToken ct)
    {
        logger.LogInformation("Getting proposal statuses start");

        var statuses = await workProposalStatusRepository.Get(ct);

        logger.LogInformation("Getting proposal statuses success");

        return statuses;
    }
}
