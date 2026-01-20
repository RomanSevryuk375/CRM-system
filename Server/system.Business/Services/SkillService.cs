using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;

namespace CRMSystem.Business.Services;

public class SkillService : ISkillService
{
    private readonly ISkillRepository _skillRepository;
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IWorkerRepository _workerRepository;
    private readonly IUserContext _userContext;
    private readonly ILogger<SkillService> _logger;

    public SkillService(
        ISkillRepository skillRepository,
        ISpecializationRepository specializationRepository,
        IWorkerRepository workerRepository,
        IUserContext userContext,
        ILogger<SkillService> logger)
    {
        _skillRepository = skillRepository;
        _specializationRepository = specializationRepository;
        _workerRepository = workerRepository;
        _userContext = userContext;
        _logger = logger;
    }

    public async Task<List<SkillItem>> GetSkills(SkillFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting skills start");

        if(_userContext.RoleId != (int)RoleEnum.Manager)
            filter = filter with { WorkerIds = [(int)_userContext.ProfileId] };

        var skills = await _skillRepository.Get(filter, ct);

        _logger.LogInformation("Getting skills success");

        return skills;
    }

    public async Task<int> GetSkillsCount(SkillFilter filter, CancellationToken ct)
    {
        _logger.LogInformation("Getting skills count start");

        var count = await _skillRepository.GetCount(filter, ct);

        _logger.LogInformation("Getting skills count success");

        return count;
    }

    public async Task<int> CreateSkill(Skill skill, CancellationToken ct)
    {
        _logger.LogInformation("Creating skill start");

        if (!await _workerRepository.Exists(skill.WorkerId, ct))
        {
            _logger.LogError("Worker {workerId} not found", skill.WorkerId);
            throw new NotFoundException($"Worker {skill.WorkerId} not found");
        }

        if (!await _specializationRepository.Exists(skill.SpecializationId, ct))
        {
            _logger.LogError("Specialization {specializationId} not found", skill.SpecializationId);
            throw new NotFoundException($"Specialization {skill.SpecializationId} not found");
        }

        var Id = await _skillRepository.Create(skill, ct);

        _logger.LogInformation("Creating skill success");

        return Id;
    }

    public async Task<int> UpdateSkill(int id, SkillUpdateModel model, CancellationToken ct)
    {
        _logger.LogInformation("Updating skill start");

        if (model.WorkerId.HasValue && !await _workerRepository.Exists(model.WorkerId.Value, ct))
        {
            _logger.LogError("Worker {workerId} not found", model.WorkerId);
            throw new NotFoundException($"Worker {model.WorkerId} not found");
        }

        if (model.SpecializationId.HasValue && !await _specializationRepository.Exists(model.SpecializationId.Value, ct))
        {
            _logger.LogError("Specialization {specializationId} not found", model.SpecializationId);
            throw new NotFoundException($"Specialization {model.SpecializationId} not found");
        }

        _logger.LogInformation("Updating skill success");

        var Id = await _skillRepository.Update(id, model, ct);

        return Id;
    }

    public async Task<int> DeleteSkill(int id, CancellationToken ct)
    {
        _logger.LogInformation("Deleting skill start");

        var Id = await _skillRepository.Delete(id, ct);

        _logger.LogInformation("Deleting skill success");

        return Id;
    }
}
