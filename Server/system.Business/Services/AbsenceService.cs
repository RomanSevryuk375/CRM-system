using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Absence;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class AbsenceService : IAbsenceService
{
    private readonly IAbsenceRepository _absenceRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly ILogger<AbsenceService> _logger;

    public AbsenceService(
        IAbsenceRepository absenceRepository,
        IWorkerRepository workerRepository,
        ILogger<AbsenceService> logger)
    {
        _absenceRepository = absenceRepository;
        _workerRepository = workerRepository;
        _logger = logger;
    }

    public async Task<List<AbsenceItem>> GetPagedAbsence(AbsenceFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting absence start");

        var absence = await _absenceRepository.GetPaged(filter, ct);

        _logger.LogInformation("Getting absence success");

        return absence;

    }

    public async Task<int> GetCountAbsence(AbsenceFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting count absence start");

        var count = await _absenceRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting count absence success");

        return count;
    }

    public async Task<int> CreateAbsence(Absence absence, CancellationToken ct)
    {
        _logger.LogInformation("Creating absence start");

        var absences = await _absenceRepository.GetByWorkerId(absence.WorkerId, ct);

        if (absences.Any(x => x.OverlapsWith(absence.StartDate, absence.EndDate)))
            throw new ConflictException($"Has date overlaps for worker{absence.WorkerId}");

        if (!await _workerRepository.Exists(absence.WorkerId, ct))
        {
            _logger.LogInformation("Worker{WorkerId} not found", absence.WorkerId);
            throw new NotFoundException($"Worker {absence.WorkerId} not found");
        }

        var abcense = await _absenceRepository.Create(absence, ct);

        _logger.LogInformation("Creating absence success");

        return abcense;
    }

    public async Task<int> UpdateAbsence(int id, AbsenceUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating absence success");

        var workerId = await _absenceRepository.GetWorkerId(id, ct)
            ?? throw new NotFoundException($"Absence {id} not found");
        var newStartDate = model.StartDate;


        if (newStartDate.HasValue)
        {
            var absences = await _absenceRepository.GetByWorkerId(workerId, ct);

            if (absences.Any(x => x.OverlapsWith(newStartDate.Value, model.EndDate)))
                throw new ConflictException($"Has date overlaps for worker{workerId}");
        }

        var absence = await _absenceRepository.Update(id, model, ct);

        _logger.LogInformation("Updating absence success");

        return absence;
    }

    public async Task<int> DeleteAbsence(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting absence success");

        var absence = await _absenceRepository.Delete(id, ct);

        _logger.LogInformation("Deleting absence success");

        return absence;
    }
}
