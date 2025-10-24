namespace CRMSystem.Core.Models;

public class Role
{
    private Role(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }

    public string Name { get; } 
}
