using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Validation;
using System.Text.RegularExpressions;

namespace CRMSystem.Core.Models;

public class Worker
{
    public Worker(int id, long userId, string name, string surname, decimal hourlyRate, string phoneNumber, string email)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Surname = surname;
        HourlyRate = hourlyRate;
        PhoneNumber = phoneNumber;  
        Email = email;
    }

    public void SetUserId (long userId)
    {
        if (userId <= 0) throw new ConflictException("Invalid ID");
        UserId = userId;
    }

    public int Id { get; }
    public long UserId { get; private set; }
    public string Name { get; }
    public string Surname { get; }
    public decimal HourlyRate { get; }
    public string PhoneNumber { get; }
    public string Email { get; }

    public static (Worker? worker, List<string> errors) Create(int id, long userId, string name, string surname, decimal hourlyRate, string phoneNumber, string email)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var userIdError = DomainValidator.ValidateId(userId, "userId");
        if (!string.IsNullOrEmpty(userIdError)) errors.Add(userIdError);

        var nameError = DomainValidator.ValidateString(name, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        var surnameError = DomainValidator.ValidateString(surname, "surname");
        if (!string.IsNullOrEmpty(surnameError)) errors.Add(surnameError);

        var phoneError = DomainValidator.ValidateString(phoneNumber, "phone");
        if (!string.IsNullOrEmpty(phoneError)) errors.Add(phoneError);

        if (!Regex.IsMatch(phoneNumber, @"^(375|80)(29|44|33|25)\d{7}$"))
            errors.Add("Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX");

        var emailError = DomainValidator.ValidateString(email, "email");
        if (!string.IsNullOrEmpty(emailError)) errors.Add(emailError);

        var hourlyRateError = DomainValidator.ValidateMoney(hourlyRate, "hourlyRate");
        if (!string.IsNullOrEmpty(hourlyRateError)) errors.Add(hourlyRateError);

        if (errors.Any())
            return (null, errors);

        var worker = new Worker(id, userId, name, surname, hourlyRate, phoneNumber, email);

        return (worker, new List<string>());

    }
}
