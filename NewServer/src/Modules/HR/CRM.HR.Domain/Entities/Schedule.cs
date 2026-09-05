using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.HR.Domain.Entities;

public sealed class Schedule : AggregateRoot<ScheduleId>, IAuditable, ISoftDeletable
{
    private Schedule(ScheduleId id, WorkerId workerId, ShiftId shiftId, DateOnly date)
    {
        Id = id;
        WorkerId = workerId;
        ShiftId = shiftId;
        Date = date;
    }

#pragma warning disable CS8618
    private Schedule() { }
#pragma warning restore CS8618

    public WorkerId WorkerId { get; private set; }
    public ShiftId ShiftId { get; private set; }
    public DateOnly Date { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Schedule> Create(
        ScheduleId id,
        WorkerId workerId,
        ShiftId shiftId,
        DateOnly date)
    {
        Schedule schedule = new(id, workerId, shiftId, date);
        schedule.IncrementVersion();

        return Result<Schedule>.Success(schedule);
    }
}
