namespace CRMSystem.Core.Models;

public class Tax
{
    public Tax(int id, string name, decimal rate, string type)
    {
        Id = id;
        Name = name;
        Rate = rate;
        Type = type;
    }
    public int Id { get; }

    public string Name { get; } 

    public decimal Rate { get; }

    public string Type { get; } 

    public static (Tax tax, string error) Create (int id, string name, decimal rate, string type)
    {
        var error = string.Empty;
        var allowedTypes = new[] { "республиканский", "внебюджетный", "местный", "целевой"};

        if (string.IsNullOrWhiteSpace(name))
            error = "Name can't be empty";
        if (name.Length > 128)
            error = $"Name can't be longer than {128} symbols";

        if (rate <= 0)
            error = "Rate can't be negative or zero";

        if (string.IsNullOrWhiteSpace(type))
            error = "Tax type can't be empty";
        if (!allowedTypes.Contains(type))
            error = "Invalid tax type";


        var tax = new Tax(id, name, rate, type);

        return (tax, error);
    }
}
