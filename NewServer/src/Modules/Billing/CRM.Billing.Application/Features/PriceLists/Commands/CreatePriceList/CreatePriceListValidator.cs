// Ignore Spelling: Validator

using CRM.Billing.Domain.Entities;
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
            .MaximumLength(PriceList.MaxNameLength);

        RuleFor(x => x.ValidFrom).NotEmpty();

        RuleFor(x => x.BaseHourlyRate)
            .GreaterThan(0m);
    }
}