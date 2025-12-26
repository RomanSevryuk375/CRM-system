using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _specializationRepository;
    private readonly ILogger<SpecializationService> _logger;

    public SpecializationService(
        ISpecializationRepository specializationRepository,
        ILogger<SpecializationService> logger)
    {
        _specializationRepository = specializationRepository;
        _logger = logger;
    }

    public async Task<List<SpecializationItem>> GetSpecializations()
    {
        _logger.LogInformation("Getting specializations start");

        var specializations = await _specializationRepository.Get();

        _logger.LogInformation("Getting specializations success");

        return specializations;
    }

    public async Task<int> CreateSpecialization(Specialization specialization)
    {
        _logger.LogInformation("Creating specialization start");

        if (await _specializationRepository.ExistsByName(specialization.Name))
        {
            _logger.LogError("Specialization {specializationId} not found", specialization.Name);
            throw new NotFoundException($"Specialization {specialization.Name} not found");
        }

        var Id = await _specializationRepository.Create(specialization);

        _logger.LogInformation("Creating specialization success");

        return Id;
    }

    public async Task<int> UpdateSpecialization(int id, string? name)
    {
        _logger.LogInformation("Updating specialization start");

        if (!string.IsNullOrEmpty(name) && await _specializationRepository.ExistsByName(name))
        {
            _logger.LogError("Specialization {specializationName} not found", name);
            throw new NotFoundException($"Specialization {name} not found");
        }

        var Id = await _specializationRepository.Update(id, name);

        _logger.LogInformation("Deleting specialization success");

        return Id;
    }

    public async Task<int> DeleteSpecialization(int id)
    {
        _logger.LogInformation("Deleting specialization start");

        var Id = await _specializationRepository.Delete(id);

        _logger.LogInformation("Deleting specialization success");

        return Id;
    }
}
