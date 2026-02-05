using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class AbsenceTypeService(
    IAbsenceTypeRepository absenceTypeRepository,
    ILogger<AbsenceTypeService> logger) : IAbsenceTypeService
{
    public async Task<List<AbsenceTypeItem>> GetAllAbsenceType(CancellationToken ct)
    {
        logger.LogInformation("Getting absenceType start");

        var absenceType = await absenceTypeRepository.GetAll(ct);

        logger.LogInformation("Getting absenceType success");

        return absenceType;
    }

    public async Task<int> CreateAbsenceType(AbsenceType absenceType, CancellationToken ct)
    {
        logger.LogInformation("Creating absenceType start");

        if (await absenceTypeRepository.GetByName(absenceType.Name, ct) is not null)
        {
            logger.LogInformation("Absence type {TypeName} is exists", absenceType.Name);
            throw new FoundException($"Absence type {absenceType.Name} is exists");
        }

        var absenceTypeRes = await absenceTypeRepository.Create(absenceType, ct);

        logger.LogInformation("Creating absenceType success");

        return absenceTypeRes;
    }

    public async Task<int> UpdateAbsenceType(int id, string name, CancellationToken ct)
    {
        logger.LogInformation("Updating absence start");

        if (await absenceTypeRepository.GetByName(name, ct) is not null)
        {
            logger.LogInformation("Absence type {TypeName} is exists", name);
            throw new ConflictException($"Absence type {name} is exists");
        }

        var absenceType = await absenceTypeRepository.Update(id, name, ct);

        logger.LogInformation("Updating absence success");

        return absenceType;
    }

    public async Task<int> DeleteAbsenceType(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting absence start");

        var absenceType = await absenceTypeRepository.Delete(id, ct);

        logger.LogInformation("Deleting absence success");

        return absenceType;
    }
}
