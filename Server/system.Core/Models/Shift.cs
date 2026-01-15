using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Shift
{
    private Shift(int id, string name, TimeOnly startAt, TimeOnly endAt)
    {
        Id = id;    
        Name = name;
        StartAt = startAt;
        EndAt = endAt;
    }

    public int Id { get; }
    public string Name { get; }
    public TimeOnly StartAt { get; }
    public TimeOnly EndAt { get; }

    public static (Shift? shift, List<string> errors) Create(int id, string name, TimeOnly startAt, TimeOnly endAt)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var nameError = DomainValidator.ValidateString(name, ValidationConstants.MAX_TYPE_NAME, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        var dateRange = DomainValidator.ValidateDateRange(startAt, endAt);
        if (!string.IsNullOrEmpty(dateRange)) errors.Add(dateRange);

        if (errors.Any())
            return (null, errors);

        var shift = new Shift(id, name, startAt, endAt);

        return (shift, new List<string>());
    }
}
