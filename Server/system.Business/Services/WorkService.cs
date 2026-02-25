using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.ProjectionModels.Work;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class WorkService(
    IWorkRepository workRepository,
    ILogger<WorkService> logger) : IWorkService
{
    public async Task<WorkItem> GetWorkById(long id, CancellationToken ct)
    {
        return await workRepository.GetById(id, ct)
               ?? throw new NotFoundException($"Work {id} not found");
    }
    
    public async Task<List<WorkItem>> GetPagedWork(WorkFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting works start");

        var works = await workRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting works success");

        return works;
    }

    public async Task<int> GetCountWork(CancellationToken ct)
    {
        logger.LogInformation("Getting works count start");

        var count = await workRepository.GetCount(ct);

        logger.LogInformation("Getting works count success");

        return count;
    }

    public async Task<long> CreateWork(WorkCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating work start");

        var (work, errors) = Work.Create(
            0,
            createModel.Title,
            createModel.Category,
            createModel.Description,
            createModel.StandardTime);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }
        
        var id = await workRepository.Create(work!, ct);

        logger.LogInformation("Creating work success");

        return id;
    }

    public async Task<long> UpdateWork(long id, WorkUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating work start");

        var workId = await workRepository.Update(id, model, ct);

        logger.LogInformation("Updating work success");

        return workId;
    }

    public async Task<long> DeleteWork(long id, CancellationToken ct)
    {
        logger.LogInformation("Deleting work start");

        var workId = await workRepository.Delete(id, ct);

        logger.LogInformation("Deleting work success");

        return workId;
    }
}
