using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class WorkInOrderStatusService(
    IWorkInOrderStatusRepository workInOrderStatusRepository,
    ILogger<WorkInOrderStatusService> logger) : IWorkInOrderStatusService
{
    public async Task<List<WorkInOrderStatusItem>> GetWiOStatuses(CancellationToken ct)
    {
        logger.LogInformation("Getting work in order statuses start");

        var statuses = await workInOrderStatusRepository.Get(ct);

        logger.LogInformation("Getting work in order statuses success");

        return statuses;
    }
}
