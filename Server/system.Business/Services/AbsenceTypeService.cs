using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class AbsenceTypeService : IAbsenceTypeService
{
    private readonly IAbsenceTypeRepository _absenceTypeRepository;
    private readonly ILogger<AbsenceTypeService> _logger;

    public AbsenceTypeService(
        IAbsenceTypeRepository absenceTypeRepository,
        ILogger<AbsenceTypeService> logger)
    {
        _absenceTypeRepository = absenceTypeRepository;
        _logger = logger;
    }

    public async Task<List<AbsenceTypeItem>> GetAllAbsenceType(CancellationToken ct)
    {
        _logger.LogInformation("Getting absenceType start");

        var absenceType = await _absenceTypeRepository.GetAll(ct);

        _logger.LogInformation("Getting absenceType success");

        return absenceType;
    }

    public async Task<int> CreateAbsenceType(AbsenceType absenceType, CancellationToken ct)
    {
        _logger.LogInformation("Creating absenceType start");

        if (await _absenceTypeRepository.GetByName(absenceType.Name, ct) is not null)
        {
            _logger.LogInformation("Absence type {TypeName} is exists", absenceType.Name);
            throw new FoundException($"Absence type {absenceType.Name} is exists");
        }

        var absenceTypeRes = await _absenceTypeRepository.Create(absenceType, ct);

        _logger.LogInformation("Creating absenceType success");

        return absenceTypeRes;
    }

    public async Task<int> UpdateAbsenceType(int id, string name, CancellationToken ct)
    {
        _logger.LogInformation("Updating absence start");

        if (await _absenceTypeRepository.GetByName(name, ct) is not null)
        {
            _logger.LogInformation("Absence type {TypeName} is exists", name);
            throw new ConflictException($"Absence type {name} is exists");
        }

        var absenceType = await _absenceTypeRepository.Update(id, name, ct);

        _logger.LogInformation("Updating absence success");

        return absenceType;
    }

    public async Task<int> DeleteAbsenceType(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting absence start");

        var absenceType = await _absenceTypeRepository.Delete(id, ct);

        _logger.LogInformation("Deleting absence success");

        return absenceType;
    }
}
