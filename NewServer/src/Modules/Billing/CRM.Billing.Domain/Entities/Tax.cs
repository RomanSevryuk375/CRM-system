using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Domain.Entities;

public sealed class Tax : AggregateRoot<TaxId>
{
    private Tax(
        TaxId id,
        Name name,
        TaxRate rate,
        TaxType typeId)
    {
        Id = id;
        Name = name;
        Rate = rate;
        Type = typeId;
    }

#pragma warning disable CS8618
    private Tax() { }
#pragma warning restore CS8618

    public Name Name { get; private set; }
    public TaxRate Rate { get; private set; }
    public TaxType Type { get; private set; }

    public static Result<Tax> Create(
        TaxId id,
        Name name,
        decimal rate,
        TaxType type)
    {
        Result<TaxRate> rateResult = TaxRate.Create(rate);
        if (rateResult.IsFailure)
        {
            return Result<Tax>.Failure(rateResult.Error);
        }

        Tax tax = new(id, name, rateResult.Value, type);

        tax.IncrementVersion();

        return Result<Tax>.Success(tax);
    }

    public Result UpdateRate(decimal rate)
    {
        Result<TaxRate> rateResult = TaxRate.Create(rate);
        if (rateResult.IsFailure)
        {
            return Result.Failure(rateResult.Error);
        }

        Rate = rateResult.Value;

        IncrementVersion();

        return Result.Success();
    }

    public Result Rename(Name name)
    {
        Name = name;

        IncrementVersion();

        return Result.Success();
    }
}
