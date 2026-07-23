using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.ValueObjects;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Domain.Entities;

public sealed class Tax : AggregateRoot<TaxId>, IHasVersion
{
    private const int MaxNameLength = 64;

    private Tax(
        TaxId id,
        string name,
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

    public string Name { get; private set; }
    public TaxRate Rate { get; private set; }
    public TaxType Type { get; private set; }

    public Guid Version { get; private set; }

    public static Result<Tax> Create(
        TaxId id,
        string name,
        decimal rate,
        TaxType type)
    {
        List<Error> errors = [];

        Result nameResult = ValidateName(name);
        if (nameResult.IsFailure)
        {
            errors.Add(nameResult.Error);
        }

        Result<TaxRate> rateResult = TaxRate.Create(rate);
        if (rateResult.IsFailure)
        {
            errors.Add(rateResult.Error);
        }

        if (errors.Count != 0)
        {
            return Result<Tax>.Failure(Error.Validation<Tax>(
                string.Join("; ", errors.Select(x => x.Message))));
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

    public Result Rename(string name)
    {
        Result nameResult = ValidateName(name);
        if (nameResult.IsFailure)
        {
            return Result.Failure(nameResult.Error);
        }

        Name = name;

        IncrementVersion();

        return Result.Success();
    }

    private void IncrementVersion()
    {
        Version = Guid.NewGuid();
    }

    private static Result ValidateName(string name)
    {
        List<Error> errors = [];
        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(Error.Validation<Tax>(
                "Name can not be empty."));
        }
        else if (name.Length > MaxNameLength)
        {
            errors.Add(Error.Validation<Tax>(
                $"Name should be shorter than {MaxNameLength} symbols."));
        }

        return errors.Count == 0
            ? Result.Success()
            : Result.Failure(Error.Validation<Tax>(string.Join("; ", errors.Select(x => x.Message))));
    }
}
