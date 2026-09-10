using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class TaxRate : ValueObject
{
    public const decimal MinPercentage = 0m;
    public const decimal MaxPercentage = 100m;
    public const int Precision = 5;
    public const int Scale = 4;

    public decimal Value { get; }

    private TaxRate(decimal value)
    {
        Value = value;
    }

    public static Result<TaxRate> Create(decimal percentage)
    {
        if (percentage < MinPercentage)
        {
            return Result<TaxRate>.Failure(Error.Validation<TaxRate>(Errors.NegativeRate));
        }

        if (percentage > MaxPercentage)
        {
            return Result<TaxRate>.Failure(Error.Validation<TaxRate>(Errors.ExceedsMax));
        }

        return Result<TaxRate>.Success(
            new TaxRate(percentage / MaxPercentage));
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
        public static readonly string ExceedsMax = $"Tax rate cannot exceed {MaxPercentage}%.";
    }
}
