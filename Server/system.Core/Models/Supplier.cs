namespace CRMSystem.Core.Models;

public class Supplier
{
    private const int MAX_NAME_LENGTH = 256;

    public Supplier(int id, string name, string contacts)
    {
        Id = id;
        Name = name;
        Contacts = contacts;
    }
    public int Id { get; }

    public string Name { get; } = string.Empty;

    public string Contacts { get; } = string.Empty;

    public static (Supplier supplier, string error) Create(int id, string name, string contacts)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
            error = "Name of supplier can't be empty";
        if (name.Length > MAX_NAME_LENGTH)
            error = $"Name of supplier can't be longer than {MAX_NAME_LENGTH} symbols";

        if (string.IsNullOrWhiteSpace(contacts))
            error = "Contacts of supplier can't be empty";

        var supplier = new Supplier(id, name, contacts);

        return (supplier, error);
    }
}
