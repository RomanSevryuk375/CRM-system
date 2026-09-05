using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.HR.Domain.Entities;

public sealed class Worker : AggregateRoot<WorkerId>, IAuditable, ISoftDeletable
{
    private readonly List<Skill> _skills = [];

    private Worker(
        WorkerId id,
        UserId userId,
        Name name,
        Name surname,
        Money hourlyRate,
        PhoneNumber phoneNumber,
        Email email)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Surname = surname;
        HourlyRate = hourlyRate;
        PhoneNumber = phoneNumber;
        Email = email;
    }

#pragma warning disable CS8618
    private Worker() { }
#pragma warning restore CS8618

    public UserId UserId { get; private set; }
    public Name Name { get; private set; }
    public Name Surname { get; private set; }
    public Money HourlyRate { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }

    public IReadOnlyList<Skill> Skills => _skills.AsReadOnly();

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Worker> Create(
        WorkerId id,
        UserId userId,
        Name name,
        Name surname,
        Money hourlyRate,
        PhoneNumber phoneNumber,
        Email email)
    {
        Worker worker = new(id, userId, name, surname, hourlyRate, phoneNumber, email);
        worker.IncrementVersion();

        return Result<Worker>.Success(worker);
    }

    public Result UpdateHourlyRate(Money newRate)
    {
        HourlyRate = newRate;
        IncrementVersion();
        return Result.Success();
    }

    public Result AddSkill(SkillId skillId, SpecializationId specializationId)
    {
        if (_skills.Exists(s => s.SpecializationId == specializationId))
        {
            return Result.Failure(Error.Conflict<Worker>(Errors.SpecializationAlreadyExists));
        }

        _skills.Add(Skill.Create(skillId, Id, specializationId).Value);
        IncrementVersion();

        return Result.Success();
    }

    public Result RemoveSkill(SkillId skillId)
    {
        Skill? skill = _skills.Find(s => s.Id == skillId);
        if (skill is null)
        {
            return Result.Success();
        }

        _skills.Remove(skill);
        IncrementVersion();

        return Result.Success();
    }

    public static class Errors
    {
        public const string SpecializationAlreadyExists = "Worker already has this specialization.";
    }
}
