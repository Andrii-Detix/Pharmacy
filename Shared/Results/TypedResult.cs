using Shared.Errors;

namespace Shared.Results;

public record Result<T> : Result
{
    private Result(bool isSuccess, T? value, Error error) : base(isSuccess, error)
    {
        Value = value;
    }

    public T? Value { get; }
    
    public static Result<T> CreateSuccess(T value) => new Result<T>(true, value, Error.None);
    public new static Result<T> CreateFailure(Error error) => new Result<T>(false, default, error);

    public static implicit operator Result<T>(T value) => CreateSuccess(value);
    public static implicit operator Result<T>(Error error) => CreateFailure(error);

    public static explicit operator T(Result<T> result)
    {
        if (result.IsFailure)
        {
            throw new InvalidOperationException("Result is failure");
        }
        
        return result.Value!;
    }
}