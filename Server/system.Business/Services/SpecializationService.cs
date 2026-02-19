using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Specialization;
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

    public async Task<int> CreateSpecialization(SpecializationCreateModel createModel, CancellationToken ct)
    {
        logger.LogInformation("Creating specialization start");

        if (await specializationRepository.ExistsByName(createModel.Name, ct))
        {
            logger.LogError("Specialization {specializationId} not found", createModel.Name);
            throw new NotFoundException($"Specialization {createModel.Name} not found");
        }
        
        var (specialization, errors) = Specialization.Create(
            0,
            createModel.Name);

        if (errors is not null && errors.Any())
        {
            throw new ValidationException(string.Join(", ", errors));
        }

        var id = await specializationRepository.Create(specialization!, ct);

        logger.LogInformation("Creating specialization success");

        return id;
    }

    public async Task<int> UpdateSpecialization(int id, string? name, CancellationToken ct)
    {
        logger.LogInformation("Updating specialization start");

        if (!string.IsNullOrEmpty(name) && await specializationRepository.ExistsByName(name, ct))
        {
            logger.LogError("Specialization {specializationName} not found", name);
            throw new NotFoundException($"Specialization {name} not found");
        }

        var specializationId = await specializationRepository.Update(id, name, ct);

        logger.LogInformation("Deleting specialization success");

        return specializationId;
    }

    public async Task<int> DeleteSpecialization(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting specialization start");

        var specializationId = await specializationRepository.Delete(id, ct);

        logger.LogInformation("Deleting specialization success");

        return specializationId;
    }
}
