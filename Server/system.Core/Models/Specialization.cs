namespace CRMSystem.Core.Models;

public class Specialization
{
    public const int MAX_NAME_LENGTH = 256;
    public Specialization(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; }

    public string Name { get; }

    public static (Specialization specialization, string error) Create (int id, string name)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
            error = "Name of specialization can't be empty";
        if (name.Length > MAX_NAME_LENGTH)
            error = $"Name can't be longer than {MAX_NAME_LENGTH} symbols";

        var specialization = new Specialization(id, name);

        return (specialization, error);
    }
}
