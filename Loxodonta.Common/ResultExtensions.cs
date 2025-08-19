namespace Loxodonta.Common;

public static class ResultExtensions
{
    //Success Value included, Failure Error included.
    public static TOut Match<TValue, TOut>(
        this Result<TValue> result,
        Func<TValue, TOut> success,
        Func<Error, TOut> failure)
    {
        return result.IsSuccess ? success(result.Value) : failure(result.Error);
    }

    //Success Value excluded, Failure Error included.
    public static TOut Match<TOut>(this Result result, Func<TOut> success, Func<Error, TOut> failure)
    {
        return result.IsSuccess ? success() : failure(result.Error);
    }
}