namespace CRMSystem.Core.Models;
using System.Text.RegularExpressions;

public class Client
{
    public const int MAX_NAME_LENGTH = 128;
    public const int MAX_SURNAME_LENGTH = 128;
    public const int MAX_EMAIL_LENGTH = 128;
    public const int MAX_PHONE_NUMBER_LENGTH = 32;
    private Client(int id, int userId, string name, string surname, string phoneNumber, string email)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public int Id { get; }

    public int UserId { get; }

    public string Name { get; } = string.Empty;

    public string Surname { get; } = string.Empty;

    public string PhoneNumber { get; } = string.Empty;

    public string Email { get; } = string.Empty;

    public static (Client client, string error) Create(int id, int userId, string name, string surname, string phoneNumber, string email)
    {
        var errors = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
        {
            errors = "Name can't be empty";
        }
        else if (name.Length > MAX_NAME_LENGTH)
        {
            errors = $"Name cant't be longer than {MAX_NAME_LENGTH} symbols";
        }

        if (string.IsNullOrWhiteSpace(surname))
        {
            errors = "Surname can't be empty";
        }
        else if (surname.Length > MAX_SURNAME_LENGTH)
        {
            errors = $"Surname can't be longer than {MAX_SURNAME_LENGTH} symbols";
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            errors = "Phone number can't be empty";
        }
        else if (phoneNumber.Length > MAX_PHONE_NUMBER_LENGTH)
        {
            errors = $"Phone number can't be longer than {MAX_PHONE_NUMBER_LENGTH} symbols";
        }
        else if (!Regex.IsMatch(phoneNumber, @"^(\375|80)(29|44|33|25)\d{7}$"))
        {
            errors = "Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX";
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            errors = "Email can't be empty";
        }
        else if (email.Length > MAX_EMAIL_LENGTH)
        {
            errors = $"Email can't be longer than {MAX_EMAIL_LENGTH} symbols";
        }

        var client = new Client(id, userId, name.Trim(), surname.Trim(), phoneNumber.Trim(), email.Trim());

        return (client, string.Empty);
    }
}
