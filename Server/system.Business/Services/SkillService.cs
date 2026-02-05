using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.ProjectionModels.Skill;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using Microsoft.Extensions.Logging;
using Shared.Enums;
using Shared.Filters;

namespace CRMSystem.Business.Services;

public class SkillService(
    ISkillRepository skillRepository,
    ISpecializationRepository specializationRepository,
    IWorkerRepository workerRepository,
    IUserContext userContext,
    ILogger<SkillService> logger) : ISkillService
{
    public async Task<List<SkillItem>> GetSkills(SkillFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting skills start");

        if (userContext.RoleId != (int)RoleEnum.Manager)
        {
            filter = filter with { WorkerIds = [(int)userContext.ProfileId] };
        }

        var skills = await skillRepository.Get(filter, ct);

        logger.LogInformation("Getting skills success");

        return skills;
    }

    public async Task<int> GetSkillsCount(SkillFilter filter, CancellationToken ct)
    {
        logger.LogInformation("Getting skills count start");

        var count = await skillRepository.GetCount(filter, ct);

        logger.LogInformation("Getting skills count success");

        return count;
    }

    public async Task<int> CreateSkill(Skill skill, CancellationToken ct)
    {
        logger.LogInformation("Creating skill start");

        if (!await workerRepository.Exists(skill.WorkerId, ct))
        {
            logger.LogError("Worker {workerId} not found", skill.WorkerId);
            throw new NotFoundException($"Worker {skill.WorkerId} not found");
        }

        if (!await specializationRepository.Exists(skill.SpecializationId, ct))
        {
            logger.LogError("Specialization {specializationId} not found", skill.SpecializationId);
            throw new NotFoundException($"Specialization {skill.SpecializationId} not found");
        }

        var Id = await skillRepository.Create(skill, ct);

        logger.LogInformation("Creating skill success");

        return Id;
    }

    public async Task<int> UpdateSkill(int id, SkillUpdateModel model, CancellationToken ct)
    {
        logger.LogInformation("Updating skill start");

        if (model.WorkerId.HasValue && !await workerRepository.Exists(model.WorkerId.Value, ct))
        {
            logger.LogError("Worker {workerId} not found", model.WorkerId);
            throw new NotFoundException($"Worker {model.WorkerId} not found");
        }

        if (model.SpecializationId.HasValue && !await specializationRepository.Exists(model.SpecializationId.Value, ct))
        {
            logger.LogError("Specialization {specializationId} not found", model.SpecializationId);
            throw new NotFoundException($"Specialization {model.SpecializationId} not found");
        }

        logger.LogInformation("Updating skill success");

        var Id = await skillRepository.Update(id, model, ct);

        return Id;
    }

    public async Task<int> DeleteSkill(int id, CancellationToken ct)
    {
        logger.LogInformation("Deleting skill start");

        var Id = await skillRepository.Delete(id, ct);

        logger.LogInformation("Deleting skill success");

        return Id;
    }
}
