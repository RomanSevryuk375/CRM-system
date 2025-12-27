using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class AbsenceType
{
    public AbsenceType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }

    public static (AbsenceType? absenceType, List<string>? error) Create (int id, string name)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "ID");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var nameErrors = DomainValidator.ValidateString(name, ValidationConstants.MAX_TYPE_NAME , "Name");
        if (!string.IsNullOrEmpty(nameErrors)) errors.Add(nameErrors);

        if (errors.Any())
            return (null,errors);

        var absenceType = new AbsenceType(id, name);

        return (absenceType, new List<string>());
    }
}
