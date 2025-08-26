namespace Loxodonta.Common;

public class Result
{
    public readonly bool IsSuccess;
    public readonly Error Error;
    internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result<TValue> Success<TValue>(TValue value)
    {
        return new Result<TValue>(true, Error.None, value);
    }

    public static Result<TValue> Failure<TValue>(Error error)
    {
        return new Result<TValue>(false, error, default);
    }

    public static Result Success()
    {
        return new Result(true, Error.None);
    }

    public static Result Failure(Error error)
    {
        return new Result(false, error);
    }

    //Success Value excluded, Failure Error included.
    public TOut Match<TOut>(Func<TOut> success, Func<Error, TOut> failure)
    {
        return IsSuccess ? success() : failure(Error);
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    internal Result(bool isSuccess, Error error, TValue? value) : base(isSuccess, error)
    {
        _value = value;
    }

    //Success Value included, Failure Error included.
    public TOut Match<TOut>(Func<TValue, TOut> success, Func<Error, TOut> failure)
    {
        return IsSuccess ? success(Value) : failure(Error);
    }
}