using CRMSystem.Buisnes.Abstractions;
using CRMSystem.Core.DTOs.Skill;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Repositories;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Buisnes.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly ILogger<SkillService> _logger;

    public SkillService(
        ISkillRepository skillRepository,
        ISpecializationRepository specializationRepository,
        IWorkerRepository workerRepository,
        ILogger<SkillService> logger)
    {
        _skillRepository = skillRepository;
        _specializationRepository = specializationRepository;
        _workerRepository = workerRepository;
        _logger = logger;
    }

    public async Task<List<SkillItem>> GetPagedSkills(SkillFilter filter)
    {
        _logger.LogInformation("Getting skills start");

        var skills = await _skillRepository.Get(filter);

        _logger.LogInformation("Getting skills success");

        return skills;
    }

    public async Task<int> GetSkillsCount(SkillFilter filter)
    {
        _logger.LogInformation("Getting skills count start");

        var count = await _skillRepository.GetCount(filter);

        _logger.LogInformation("Getting skills count success");

        return count;
    }

    public async Task<int> CreateSkill(Skill skill)
    {
        _logger.LogInformation("Cretaing skill start");

        if (!await _workerRepository.Exists(skill.WorkerId))
        {
            _logger.LogError("Worker {workerId} not found", skill.WorkerId);
            throw new NotFoundException($"Worker {skill.WorkerId} not found");
        }

        if (!await _specializationRepository.Exists(skill.SpecializationId))
        {
            _logger.LogError("Specialization {specializationId} not found", skill.SpecializationId);
            throw new NotFoundException($"Specialization {skill.SpecializationId} not found");
        }

        var Id = await _skillRepository.Create(skill);

        _logger.LogInformation("Cretaing skill success");

        return Id;
    }

    public async Task<int> UpdateSkill(int id, SkillUpdateModel model)
    {
        _logger.LogInformation("Updating skill start");

        if (model.workerId.HasValue && !await _workerRepository.Exists(model.workerId.Value))
        {
            _logger.LogError("Worker {workerId} not found", model.workerId);
            throw new NotFoundException($"Worker {model.workerId} not found");
        }

        if (model.specializationId.HasValue && !await _specializationRepository.Exists(model.specializationId.Value))
        {
            _logger.LogError("Specialization {specializationId} not found", model.specializationId);
            throw new NotFoundException($"Specialization {model.specializationId} not found");
        }

        _logger.LogInformation("Updating skill success");

        var Id = await _skillRepository.Update(id, model);

        return Id;
    }

    public async Task<int> DeleteSkill(int id)
    {
        _logger.LogInformation("Deleting skill start");

        var Id = await _skillRepository.Delete(id);

        _logger.LogInformation("Deleting skill success");

        return Id;
    }
}
