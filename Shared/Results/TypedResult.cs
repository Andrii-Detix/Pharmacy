using Shared.Errors;

namespace Shared.Results;

public record Result<T> : Result
{
    protected Result(bool isSuccess, T? value, Error? error) : base(isSuccess, error)
    {
        if (isSuccess && value is null || !isSuccess && value is not null)
        {
            throw new ArgumentException("Invalid value", nameof(value));
        }
        
        Value = value;
    }

    public T? Value { get; }
    
    public static Result<T> CreateSuccess(T value) => new Result<T>(true, value, null);
    public static Result<T> CreateFailure<T>(Error error) => new Result<T>(false, default, error);
}