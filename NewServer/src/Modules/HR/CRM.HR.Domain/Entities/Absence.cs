using CRM.HR.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.HR.Domain.Entities;

public sealed class Absence : AggregateRoot<AbsenceId>, IAuditable, ISoftDeletable
{
    private Absence(
        AbsenceId id,
        WorkerId workerId,
        AbsenceType type,
        DateOnly startDate,
        DateOnly? endDate)
    {
        Id = id;
        WorkerId = workerId;
        Type = type;
        StartDate = startDate;
        EndDate = endDate;
    }

#pragma warning disable CS8618
    private Absence() { }
#pragma warning restore CS8618

    public WorkerId WorkerId { get; private set; }
    public AbsenceType Type { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Absence> Create(
        AbsenceId id,
        WorkerId workerId,
        AbsenceType type,
        DateOnly startDate,
        DateOnly? endDate)
    {
        if (endDate.HasValue && endDate.Value < startDate)
        {
            return Result<Absence>.Failure(Error.Validation<Absence>(Errors.InvalidDateRange));
        }

        Absence absence = new(id, workerId, type, startDate, endDate);
        absence.IncrementVersion();

        return Result<Absence>.Success(absence);
    }

    public static class Errors
    {
        public const string InvalidDateRange = "End date cannot be before start date.";
    }
}
