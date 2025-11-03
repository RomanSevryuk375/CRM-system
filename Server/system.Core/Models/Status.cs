namespace CRMSystem.Core.Models;

public class Status
{
    public Status(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }
    public int Id { get; }

    public string Name { get; } 

    public string Description { get; }

    public static(Status status, string error) Create (int id, string name, string description)
    {
        var error = string.Empty;
        var status = new Status(id, name, description);

        return (status, error);
    }
}
