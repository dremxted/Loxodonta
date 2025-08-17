namespace Loxodonta.Common;

public sealed record Error
{
    public readonly string Code;
    public readonly string Message;
    public readonly ErrorType? Type;
    private readonly object[] _extensions;
    public IReadOnlyCollection<object> Extensions => _extensions;

    internal Error(ErrorType? errorType, string code, string message, object[] extensions)
    {
        Type = errorType;
        Code = code;
        Message = message;
        _extensions = extensions;
    }

    public static readonly Error None = new Error(null, string.Empty, string.Empty, []);
    public static Error NotFound(string code, string description, params object[] extensions) =>
        new Error(ErrorType.NotFound, code, description, extensions);
    public static Error Failure(string code, string description, params object[] extensions) =>
        new Error(ErrorType.Failure, code, description, extensions);

    public static Error Validation(string code, string description, params object[] extensions) =>
        new Error(ErrorType.Validation, code, description, extensions);
}

public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3
}