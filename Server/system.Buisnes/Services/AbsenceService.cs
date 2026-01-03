using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<AbsenceItem>> GetPagedAbsence(AbsenceFilter filter)
    {
        _logger.LogInformation("Getting absence start");

        var absence = await _absenceRepository.GetPaged(filter);

        _logger.LogInformation("Getting absence success");

        return absence;

    }

    public async Task<int> GetCountAbsence(AbsenceFilter filter)
    {
        _logger.LogInformation("Getting count absence start");

        var count = await _absenceRepository.GetCount(filter);

        _logger.LogInformation("Getting count absence success");

        return count;
    }

    public async Task<int> CreateAbsence(Absence absence)
    {
        _logger.LogInformation("Creating absence start");

        if(await _absenceRepository.HasOverLap(absence.WorkerId, absence.StartDate, absence.EndDate))
        {
            _logger.LogInformation("Has date overlaps for worker{WorkerId}", absence.WorkerId);
            throw new ConflictException($"Has date overlaps for worker{absence.WorkerId}");
        }

        if (!await _workerRepository.Exists(absence.WorkerId))
        {
            _logger.LogInformation("Worker{WorkerId} not found", absence.WorkerId);
            throw new NotFoundException($"Worker {absence.WorkerId} not found");
        }

        var abcense = await _absenceRepository.Create(absence);

        _logger.LogInformation("Creating absence success");

        return abcense;
    }

    public async Task<int> UpdateAbsence(int id, AbsenceUpdateModel model)
    {
        _logger.LogInformation("Updating absence success");

        var workerId = await _absenceRepository.GetWorkerId(id);
        if (workerId is null) 
            throw new NotFoundException($"Absence {id} not found");

        var newStartDate = model.StartDate;


        if (newStartDate.HasValue)
        {
            if (await _absenceRepository.HasOverLap(workerId.Value, newStartDate.Value, model.EndDate, id))
            {
                _logger.LogInformation("Has date overlaps for worker{WorkerId}", id);
                throw new ConflictException($"Has date overlaps for worker{id}");
            }
        }

        var absence = await _absenceRepository.Update(id, model);

        _logger.LogInformation("Updating absence success");

        return absence;
    }

    public async Task<int> DeleteAbsence(int id)
    {
        _logger.LogInformation("Deleting absence success");

        var absence = await _absenceRepository.Delete(id);

        _logger.LogInformation("Deleting absence success");

        return absence;
    }
}
