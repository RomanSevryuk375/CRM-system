using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Absence;
using CRMSystem.Core.DTOs.Worker;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;

namespace CRMSystem.Buisnes.Services;

public class AbsenceService : IAbsenceService
{
    private readonly IAbsenceRepository _absenceRepository;
    private readonly IWorkerRepository _workerRepository;

    public AbsenceService(
        IAbsenceRepository absenceRepository,
        IWorkerRepository workerRepository)
    {
        _absenceRepository = absenceRepository;
        _workerRepository = workerRepository;
    }

    public async Task<List<AbsenceItem>> GetPagedAbsence(AbsenceFilter filter)
    {
        var absence = await _absenceRepository.GetPaged(filter);

        return absence;

    }

    public async Task<int> GetCountAbsence(AbsenceFilter filter)
    {
        return await _absenceRepository.GetCount(filter);
    }

    public async Task<int> CreateAbsence(Absence absence)
    {
        // time check date ocerlps

        var workerFilter = new WorkerFilter
        (
            new[] { absence.WorkerId },
            null,
            1,
            5,
            true
        );

        var worker = await _workerRepository.GetPaged(workerFilter)
            ?? throw new Exception($"Worker {absence.WorkerId} not found");

        var abcense = await _absenceRepository.Create(absence);

        return abcense;
    }

    public async Task<int> UpdateAbsence(AbsenceUpdateModel model)
    {
        return await _absenceRepository.Update(model);
    }

    public async Task<int> DeleteAbsence(int id)
    {
        return await _absenceRepository.Delete(id);
    }
}
