using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Specialization
{
    public Specialization(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; }

    public string Name { get; }

    public static (Specialization? specialization, List<string> errors) Create (int id, string name)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var nameError = DomainValidator.ValidateString(name, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        if (errors.Any())
            return (null , errors);

        var specialization = new Specialization(id, name);

        return (specialization, new List<string>());
    }
}
