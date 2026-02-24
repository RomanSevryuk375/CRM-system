using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Client;
using Shared.Enums;

namespace CRMSystem.Business.Validators.ClientValidator;

public class ClientRegisterRequestValidator : AbstractValidator<ClientRegisterRequest>
{
    public ClientRegisterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Surname)
            .NotEmpty()
                .WithMessage("Surname should not be empty")
            .MaximumLength(ValidationConstants.MAX_SURNAME_LENGTH)
                .WithMessage($"Surname should be shorter than {ValidationConstants.MAX_SURNAME_LENGTH}");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
                .WithMessage("Phone number should not be empty")
            .Matches(@"^(\+375|80)(29|44|33|25)\d{7}$")
                .WithMessage("Phone number should be in format +375XXXXXXXXX or 80XXXXXXXXX");
        
        RuleFor(x => x.RoleId)
            .Equal((int)RoleEnum.Client)
                .WithMessage("RoleId should be 'Client' for self-registration");

        RuleFor(x => x.Login)
            .NotEmpty()
                .WithMessage("Login should not be empty")
            .MinimumLength(3)
                .WithMessage("Login should be at least 3 characters long");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Password should not be empty")
            .MinimumLength(6)
                .WithMessage("Password should be at least 6 characters long");
                
        RuleFor(x => x.Email)
            .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Email should be a valid email address");
    }
}