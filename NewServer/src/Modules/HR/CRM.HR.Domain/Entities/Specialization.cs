using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.HR.Domain.Entities;

public sealed class Specialization : AggregateRoot<SpecializationId>, IAuditable, ISoftDeletable
{
    private Specialization(SpecializationId id, Name name)
    {
        Id = id;
        Name = name;
    }

#pragma warning disable CS8618
    private Specialization() { }
#pragma warning restore CS8618

    public Name Name { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Specialization> Create(SpecializationId id, Name name)
    {
        Specialization specialization = new(id, name);
        specialization.IncrementVersion();

        return Result<Specialization>.Success(specialization);
    }

    public Result UpdateName(Name newName)
    {
        Name = newName;
        IncrementVersion();

        return Result.Success();
    }
}
