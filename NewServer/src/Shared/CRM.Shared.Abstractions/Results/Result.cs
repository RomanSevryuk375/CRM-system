using CRM.Shared.Abstractions.Constants;

namespace CRM.Shared.Abstractions.Results;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if (ErrorInvalid(isSuccess, error))
        {
            throw new ArgumentException(ResultErrors.InvalidErrorState, nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;

        static bool ErrorInvalid(bool isSuccess, Error error)
        {
            return isSuccess && error != Error.None || !isSuccess && error == Error.None;
        }
    }

    public static Result Success()
    {
        return new(true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new(false, error);
    }
}

public class Result<T> : Result
{
    private readonly T? _value;

    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException(ResultErrors.ResultIsFailure);

    private Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public static Result<T> Success(T value)
    {
        return new(value, true, Error.None);
    }

    public static new Result<T> Failure(Error error)
    {
        return new(default, false, error);
    }

    public static implicit operator Result<T>(T value)
    {
        return Success(value);
    }
}
