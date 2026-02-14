using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Absence;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class AbsenceService(
    IAbsenceRepository absenceRepository,
    IWorkerRepository workerRepository,
    IUserContext userContext,
    ILogger<AbsenceService> logger) : IAbsenceService
{
    public async Task<List<AbsenceItem>> GetPagedAbsence(AbsenceFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting absence start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { WorkerIds = [(int)userContext.ProfileId] };
        }

        var absence = await absenceRepository.GetPaged(filter, ct);

        logger.LogInformation("Getting absence success");

        return absence;
    }

    public async Task<int> GetCountAbsence(AbsenceFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting count absence start");

        var count = await absenceRepository.GetCount(filter, ct);

        logger.LogInformation("Getting count absence success");

        return count;
    }

    public async Task<int> CreateAbsence(Absence absence, CancellationToken ct)
    {
        logger.LogInformation("Creating absence start");

        var absences = await absenceRepository.GetByWorkerId(absence.WorkerId, ct);

        if (absences.Any(x => x!.OverlapsWith(absence.StartDate, absence.EndDate)))
        {
            throw new ConflictException($"Has date overlaps for worker{absence.WorkerId}");
        }

        if (!await workerRepository.Exists(absence.WorkerId, ct))
        {
            logger.LogInformation("Worker{WorkerId} not found", absence.WorkerId);
            throw new NotFoundException($"Worker {absence.WorkerId} not found");
        }

        var absenceId = await absenceRepository.Create(absence, ct);

        logger.LogInformation("Creating absence success");

        return absenceId;
    }

    public async Task<int> UpdateAbsence(int id, AbsenceUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating absence success");

        var workerId = await absenceRepository.GetWorkerId(id, ct)
            ?? throw new NotFoundException($"Absence {id} not found");
        var newStartDate = model.StartDate;


        if (newStartDate.HasValue)
        {
            var absences = await absenceRepository.GetByWorkerId(workerId, ct);

            if (absences.Any(x => x!.OverlapsWith(newStartDate.Value, model.EndDate)))
            {
                throw new ConflictException($"Has date overlaps for worker{workerId}");
            }
        }

        var absence = await absenceRepository.Update(id, model, ct);

        logger.LogInformation("Updating absence success");

        return absence;
    }

    public async Task<int> DeleteAbsence(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting absence success");

        var absence = await absenceRepository.Delete(id, ct);

        logger.LogInformation("Deleting absence success");

        return absence;
    }
}
