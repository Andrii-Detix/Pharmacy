﻿using Shared.Errors;

namespace Shared.Results;

public record Result
{
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }
        
        IsSuccess = isSuccess;
        Error = error;
    }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result CreateSuccess() => new Result(true, Error.None);
    public static Result CreateFailure(Error error) => new Result(false, error);
    
    public static implicit operator Result(Error error) => CreateFailure(error);
}