using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Supplier
{
    private Supplier(int id, string name, string contacts)
    {
        Id = id;
        Name = name;
        Contacts = contacts;
    }
    public int Id { get; }
    public string Name { get; } 
    public string Contacts { get; } 

    public static (Supplier? supplier, List<string> errors) Create(int id, string name, string contacts)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var nameError = DomainValidator.ValidateString(name, ValidationConstants.MAX_NAME_LENGTH, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        var contactsError = DomainValidator.ValidateString(contacts, ValidationConstants.MAX_DESCRIPTION_LENGTH, "contacts");
        if (!string.IsNullOrEmpty(contactsError)) errors.Add(contactsError);

        if (errors.Any())
            return (null, errors);

        var supplier = new Supplier(id, name, contacts);

        return (supplier, new List<string>());
    }
}
