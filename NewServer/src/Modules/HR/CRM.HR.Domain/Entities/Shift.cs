using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.HR.Domain.Entities;

public sealed class Shift : AggregateRoot<ShiftId>, IAuditable, ISoftDeletable
{
    private Shift(ShiftId id, Name name, TimeOnly startAt, TimeOnly endAt)
    {
        Id = id;
        Name = name;
        StartAt = startAt;
        EndAt = endAt;
    }

#pragma warning disable CS8618
    private Shift() { }
#pragma warning restore CS8618

    public Name Name { get; private set; }
    public TimeOnly StartAt { get; private set; }
    public TimeOnly EndAt { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Shift> Create(
        ShiftId id,
        Name name,
        TimeOnly startAt,
        TimeOnly endAt)
    {

        if (startAt >= endAt)
        {
            return Result<Shift>.Failure(Error.Validation<Shift>(Errors.InvalidTimeRange));
        }

        Shift shift = new(id, name, startAt, endAt);
        shift.IncrementVersion();

        return Result<Shift>.Success(shift);
    }

    public static class Errors
    {
        public const string InvalidTimeRange = "Start time must be before end time.";
    }
}
