using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Business.Services;

public class SpecializationService(
    ISpecializationRepository specializationRepository,
    ILogger<SpecializationService> logger) : ISpecializationService
{
    public async Task<List<SpecializationItem>> GetSpecializations(CancellationToken ct)
    {
        logger.LogInformation("Getting specializations start");

        var specializations = await specializationRepository.Get(ct);

        logger.LogInformation("Getting specializations success");

        return specializations;
    }

    public async Task<int> CreateSpecialization(Specialization specialization, CancellationToken ct)
    {
        logger.LogInformation("Creating specialization start");

        if (await specializationRepository.ExistsByName(specialization.Name, ct))
        {
            logger.LogError("Specialization {specializationId} not found", specialization.Name);
            throw new NotFoundException($"Specialization {specialization.Name} not found");
        }

        var Id = await specializationRepository.Create(specialization, ct);

        logger.LogInformation("Creating specialization success");

        return Id;
    }

    public async Task<int> UpdateSpecialization(int id, string? name, CancellationToken ct)
    {
        logger.LogInformation("Updating specialization start");

        if (!string.IsNullOrEmpty(name) && await specializationRepository.ExistsByName(name, ct))
        {
            logger.LogError("Specialization {specializationName} not found", name);
            throw new NotFoundException($"Specialization {name} not found");
        }

        var Id = await specializationRepository.Update(id, name, ct);

        logger.LogInformation("Deleting specialization success");

        return Id;
    }

    public async Task<int> DeleteSpecialization(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting specialization start");

        var Id = await specializationRepository.Delete(id, ct);

        logger.LogInformation("Deleting specialization success");

        return Id;
    }
}
