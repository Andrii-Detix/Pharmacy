using Pharmacy.Domain.Errors;

namespace Pharmacy.Domain.Results;

public record Result
{
    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null || !isSuccess && error is null)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; init; }

    public static Result CreateSuccess() => new Result(true, null);
    public static Result CreateFailure(Error error) => new Result(false, error);
    
}