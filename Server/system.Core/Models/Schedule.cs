using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Schedule
{
    private Schedule(int id, int workerId, int shiftId, DateTime dateTime)
    {
        Id = id;
        WorkerId = workerId;
        ShiftId = shiftId;
        Date = dateTime;
    }

    public void SetShiftId(int shiftId)
    {
        if (shiftId <= 0) throw new ConflictException("Invalid ID");
        ShiftId = shiftId;
    }

    public int Id { get; }
    public int WorkerId { get; }
    public int ShiftId { get; private set; }
    public DateTime Date { get; }

    public static (Schedule? schedule, List<string> errors) Create(int id, int workerId, int shiftId, DateTime dateTime)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var workerIdError = DomainValidator.ValidateId(workerId, "workerId");
        if (!string.IsNullOrEmpty(workerIdError)) errors.Add(workerIdError);

        var shiftIdError = DomainValidator.ValidateId(shiftId, "shiftId");
        if (!string.IsNullOrEmpty(shiftIdError)) errors.Add(shiftIdError);

        var dateError = DomainValidator.ValidateDate(dateTime, "date");
        if (!string.IsNullOrEmpty(dateError)) errors.Add(dateError);

        if (errors.Any())
            return (null, errors);

        var schedule = new Schedule(id, workerId, shiftId, dateTime);

        return (schedule, new List<string>());
    }
}
