using CRMSystem.Core.Enums;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Absence
{
    public Absence(int id, int workerId, AbsenceTypeEnum typeId, DateOnly startDate, DateOnly? endDate)
    {
        Id = id;
        WorkerId = workerId;
        TypeId = typeId;
        StartDate = startDate;
        EndDate = endDate;
    }
    public int Id { get; }
    public int WorkerId { get; }
    public AbsenceTypeEnum TypeId { get; }
    public DateOnly StartDate { get; }
    public DateOnly? EndDate { get; }

    public (Absence? absence, List<string>? error) Create(int id, int workerId, AbsenceTypeEnum typeId, DateOnly startDate, DateOnly? endDate)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "Absence ID");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var workerIdError = DomainValidator.ValidateId(workerId, "Worker ID");
        if (!string.IsNullOrEmpty(workerIdError)) errors.Add(workerIdError);

        var typeIdError = DomainValidator.ValidateId(typeId, "Type ID");
        if (!string.IsNullOrEmpty(typeIdError)) errors.Add(typeIdError);

        var endDateError = DomainValidator.ValidateDateRange(startDate, endDate);
        if (!string.IsNullOrEmpty(endDateError)) errors.Add(endDateError);

        if (errors.Any())
        {
            return (null, errors);
        }

        var absence = new Absence(id, workerId, typeId, startDate, endDate);

        return (absence, new List<string>());
    }
}
