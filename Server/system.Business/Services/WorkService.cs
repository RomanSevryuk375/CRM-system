using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;
    private readonly ILogger<WorkService> _logger;

    public WorkService(
        IWorkRepository workRepository,
        ILogger<WorkService> logger)
    {
        _workRepository = workRepository;
        _logger = logger;
    }

    public async Task<List<WorkItem>> GetPagedWork(WorkFilter filter)
    {
        _logger.LogInformation("Getting works start");

        var works = await _workRepository.GetPaged(filter);

        _logger.LogInformation("Getting works success");

        return works;
    }

    public async Task<int> GetCountWork()
    {
        _logger.LogInformation("Getting works count start");

        var count = await _workRepository.GetCount();

        _logger.LogInformation("Getting works count success");

        return count;
    }

    public async Task<long> CreateWork(Work work)
    {
        _logger.LogInformation("Creating work start");

        var Id = await _workRepository.Create(work);

        _logger.LogInformation("Creating work success");

        return Id;
    }

    public async Task<long> UpdateWork(long id, WorkUpdateModel model)
    {
        _logger.LogInformation("Updating work start");

        var Id = await _workRepository.Update(id, model);

        _logger.LogInformation("Updating work success");

        return Id;
    }

    public async Task<long> DeleteWork(long id)
    {
        _logger.LogInformation("Deleting work start");

        var Id = await _workRepository.Delete(id);

        _logger.LogInformation("Deleting work success");

        return Id;
    }
}
