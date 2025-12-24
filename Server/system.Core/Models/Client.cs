namespace CRMSystem.Core.Models;

using CRMSystem.Core.Constants;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Validation;
using System.Text.RegularExpressions;

public class Client
{
    private Client(long id, long userId, string name, string surname, string phoneNumber, string email)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    public void SetUserId(long userId)
    {
        if (userId <= 0) throw new ConflictException("Invalid ID");
        UserId = userId;
    }

    public long Id { get; }
    public long UserId { get; private set; }
    public string Name { get; } 
    public string Surname { get; } 
    public string PhoneNumber { get; } 
    public string Email { get; } 

    public static (Client? client, List<string>? errors) Create(long id, long userId, string name, string surname, string phoneNumber, string email)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var userIdError = DomainValidator.ValidateId(userId, "userId");
        if (!string.IsNullOrEmpty(userIdError)) errors.Add(userIdError);

        var nameError = DomainValidator.ValidateString(name, ValidationConstants.MAX_NAME_LENGTH, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        var surnameError = DomainValidator.ValidateString(surname, ValidationConstants.MAX_SURNAME_LENGTH, "surname");
        if (!string.IsNullOrEmpty(surnameError)) errors.Add(surnameError);

        var phoneError = DomainValidator.ValidateString(phoneNumber, ValidationConstants.MAX_PHONE_LENGTH, "phoneNumber");
        if (!string.IsNullOrEmpty(phoneError)) errors.Add(phoneError);

        if (!Regex.IsMatch(phoneNumber, @"^(375|80)(29|44|33|25)\d{7}$"))
            errors.Add("Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX");

        var emailError = DomainValidator.ValidateString(email, ValidationConstants.MAX_EMAIL_LENGTH, "email");
        if (!string.IsNullOrEmpty(emailError)) errors.Add(emailError);

        if (errors.Any())
            return (null, errors);

        var client = new Client(id, userId, name.Trim(), surname.Trim(), phoneNumber.Trim(), email.Trim());

        return (client, new List<string>());
    }
}
