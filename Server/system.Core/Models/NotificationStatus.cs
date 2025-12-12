using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class NotificationStatus
{
    public NotificationStatus(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }

    public static (NotificationStatus? notificationStatus, List<string>? errors) Create(int id, string name)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var nameError = DomainValidator.ValidateString(name, ValidationConstants.MAX_TYPE_NAME, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        if (errors.Any())
            return (null, errors);

        var notificationStatus = new NotificationStatus(id, name);

        return (notificationStatus, new List<string>());
    }
}
