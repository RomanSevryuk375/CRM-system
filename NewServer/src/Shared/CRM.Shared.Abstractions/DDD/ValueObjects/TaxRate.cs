using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class TaxRate : ValueObject
{
    public decimal Value { get; }

    private TaxRate(decimal value)
    {
        Value = value;
    }

    public static Result<TaxRate> Create(decimal percentage)
    {
        if (percentage < 0)
        {
            return Result<TaxRate>.Failure(Error.Validation<TaxRate>(Errors.NegativeRate));
        }

        if (percentage > 100)
        {
            return Result<TaxRate>.Failure(Error.Validation<TaxRate>(Errors.ExceedsMax));
        }

        return Result<TaxRate>.Success(
            new TaxRate(percentage / 100m));
    }

    public Money CalculateAmount(Money baseAmount)
    {
        return Money.Create(baseAmount.Value * Value).Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static class Errors
    {
        public const string NegativeRate = "Tax rate cannot be negative.";
        public const string ExceedsMax = "Tax rate cannot exceed 100%.";
    }
}
