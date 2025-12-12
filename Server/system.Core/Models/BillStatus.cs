using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class BillStatus
{
    public BillStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }

    public static (BillStatus? billStatus, List<string>? errors) Create (int id, string name)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "Id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var nameError = DomainValidator.ValidateString(name, ValidationConstants.MAX_STATUS_NAME, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        if (errors.Any())
            return (null, errors);

        var billStatus = new BillStatus(id, name);

        return (billStatus, new List<string>());
    }
}
