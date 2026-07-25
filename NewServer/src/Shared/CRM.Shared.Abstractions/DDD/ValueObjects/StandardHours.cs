using CRM.Shared.Abstractions.Results;

namespace CRM.Shared.Abstractions.DDD.ValueObjects;

public sealed class StandardHours : ValueObject
{
    private const int MinValue = 0;
    private const int MaxValue = 1000;

    public decimal Value { get; }

    private StandardHours(decimal value)
    {
        Value = value;
    }

    public static Result<StandardHours> Create(decimal value)
    {
        if (value < MinValue)
        {
            return Result<StandardHours>.Failure(Error.Validation<StandardHours>(
                "Time spent cannot be negative."));
        }

        if (value > MaxValue)
        {
            return Result<StandardHours>.Failure(Error.Validation<StandardHours>(
                "Time spent exceeds logical limits."));
        }

        return Result<StandardHours>.Success(new StandardHours(value));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}