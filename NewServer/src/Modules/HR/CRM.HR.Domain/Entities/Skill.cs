using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.HR.Domain.Entities;

public sealed class Skill : Entity<SkillId>
{
    internal Skill(
        SkillId id,
        WorkerId workerId,
        SpecializationId specializationId)
    {
        Id = id;
        WorkerId = workerId;
        SpecializationId = specializationId;
    }

#pragma warning disable CS8618
    private Skill() { }
#pragma warning restore CS8618

    public WorkerId WorkerId { get; private set; }
    public SpecializationId SpecializationId { get; private set; }

    internal static Result<Skill> Create(
        SkillId id,
        WorkerId workerId,
        SpecializationId specializationId)
    {
        return Result<Skill>.Success(new Skill(id, workerId, specializationId));
    }
}
