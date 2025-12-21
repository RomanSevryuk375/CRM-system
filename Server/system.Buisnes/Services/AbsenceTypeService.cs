using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

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

    public async Task<List<AbsenceTypeItem>> GetAllAbsenceType()
    {
        _logger.LogInformation("Getting absenceType start");

        var absenceType = await _absenceTypeRepository.GetAll();

        _logger.LogInformation("Getting absenceType success");

        return absenceType;
    }

    public async Task<int> CretaeAbsenceType(AbsenceType absenceType)
    {
        _logger.LogInformation("Creating absenceType start");

        if (await _absenceTypeRepository.GetByName(absenceType.Name) is not null)
        {
            _logger.LogInformation("Absence type {TypeName} is exists", absenceType.Name);
            throw new FoundException($"Absece type {absenceType.Name} is exists");
        }

        var absenceTypeRes = await _absenceTypeRepository.Create(absenceType);

        _logger.LogInformation("Creating absenceType success");

        return absenceTypeRes;
    }

    public async Task<int> UpdateAbsenceType(int id, string name)
    {
        _logger.LogInformation("Updating absence start");

        if (await _absenceTypeRepository.GetByName(name) is not null)
        {
            _logger.LogInformation("Absence type {TypeName} is exists", name);
            throw new ConflictException($"Absece type {name} is exists");
        }

        var absenceType = await _absenceTypeRepository.Update(id, name);

        _logger.LogInformation("Updating absence success");

        return absenceType;
    }

    public async Task<int> DeleteAbsenceType(int id)
    {
        _logger.LogInformation("Deleting absence start");

        var absenceType = await _absenceTypeRepository.Delete(id);

        _logger.LogInformation("Deleting absence success");

        return absenceType;
    }
}
