// Ignore Spelling: Validator

using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentValidation;

namespace CRM.Billing.Application.Features.PriceLists.Commands.CreatePriceList;

public sealed class CreatePriceListValidator
    : AbstractValidator<CreatePriceListCommand>
{
    public CreatePriceListValidator()
    {
        RuleFor(x => x.PriceListId)
            .Empty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Name.MaxLength);

        RuleFor(x => x.ValidFrom).NotEmpty();

        RuleFor(x => x.BaseHourlyRate)
            .GreaterThan(0m);
    }
}