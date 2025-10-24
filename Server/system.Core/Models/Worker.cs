using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRMSystem.Core.Models;

public class Worker
{
    public const int MAX_NAME_LENGTH = 128;
    public const int MAX_SURNAME_LENGTH = 128;
    public const int MAX_EMAIL_LENGTH = 128;
    public const int MAX_PHONE_NUMBER_LENGTH = 32;
    public Worker(int id, int userId, int specializationId, string name, string surname, decimal hourlyRate, string phoneNumber, string email)
    {
        Id = id;
        UserId = userId;
        SpecializationId = specializationId;
        Name = name;
        Surname = surname;
        HourlyRate = hourlyRate;
        PhoneNumber = phoneNumber;  
        Email = email;
    }
    public int Id { get; }

    public int UserId { get; }

    public int SpecializationId { get; }

    public string Name { get; }

    public string Surname { get; }

    public decimal HourlyRate { get; }

    public string PhoneNumber { get; } 

    public string Email { get; } 

    public static (Worker worker, string error) Create (int id, int userId, int specializationId, string name, string surname, decimal hourlyRate, string phoneNumber, string email)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
            error = "Name can't be empty";

        if (name.Length > MAX_NAME_LENGTH)
            error = $"Name cant't be longer than {MAX_NAME_LENGTH} symbols";

        if (string.IsNullOrWhiteSpace(surname))
            error = "Surname can't be empty";

        if (surname.Length > MAX_SURNAME_LENGTH)
            error = $"Surname can't be longer than {MAX_SURNAME_LENGTH} symbols";

        if (string.IsNullOrWhiteSpace(phoneNumber))
            error = "Phone number can't be empty";

        if (phoneNumber.Length > MAX_PHONE_NUMBER_LENGTH)
            error = $"Phone number can't be longer than {MAX_PHONE_NUMBER_LENGTH} symbols";

        if (!Regex.IsMatch(phoneNumber, @"^(\375|80)(29|44|33|25)\d{7}$"))
            error = "Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX";

        if (string.IsNullOrWhiteSpace(email))
            error = "Email can't be empty";

        if (email.Length > MAX_EMAIL_LENGTH)
            error = $"Email can't be longer than {MAX_EMAIL_LENGTH} symbols";

        if (hourlyRate <= 0)
            error = "Hourly rate can't be negative or zero";

        var worker = new Worker(id, userId, specializationId, name, surname, hourlyRate, phoneNumber, email);

        return (worker, error);

    }
}
