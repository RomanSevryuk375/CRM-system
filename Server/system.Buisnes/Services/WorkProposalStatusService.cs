using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class WorkProposalStatusService : IWorkProposalStatusService
{
    private readonly IWorkProposalStatusRepository _workProposalStatusRepository;
    private readonly ILogger<WorkProposalStatusService> _logger;

    public WorkProposalStatusService(
        IWorkProposalStatusRepository workProposalStatusRepository,
        ILogger<WorkProposalStatusService> logger)
    {
        _workProposalStatusRepository = workProposalStatusRepository;
        _logger = logger;
    }

    public async Task<List<WorkProposalStatusItem>> GetProposalStatuses()
    {
        _logger.LogInformation("Getting proposal statses start");

        var statuses = await _workProposalStatusRepository.Get();

        _logger.LogInformation("Getting proposal statses success");

        return statuses;
    }
}
