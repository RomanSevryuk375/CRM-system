using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class WorkInOrderStatusService : IWorkInOrderStatusService
{
    private readonly IWorkInOrderStatusRepository _workInOrderStatusRepository;
    private readonly ILogger<WorkInOrderStatusService> _logger;

    public WorkInOrderStatusService(
        IWorkInOrderStatusRepository workInOrderStatusRepository,
        ILogger<WorkInOrderStatusService> logger)
    {
        _workInOrderStatusRepository = workInOrderStatusRepository;
        _logger = logger;
    }

    public async Task<List<WorkInOrderStatusItem>> GetWiOStatuses(CancellationToken ct)
    {
        _logger.LogInformation("Getting work in order statuses start");

        var statuses = await _workInOrderStatusRepository.Get(ct);

        _logger.LogInformation("Getting work in order statuses success");

        return statuses;
    }
}
