using Microsoft.AspNetCore.Mvc;
using Shared.Errors;

namespace Pharmacy.Web.Extensions.ErrorExtensions;

public static class ErrorExtension
{
    public static ObjectResult ToProblemDetails(this Error error)
    {
        var problemDetails = new ProblemDetails()
        {
            Status = GetStatusCode(error.Type),
            Title = GetTitle(error.Type),
            Detail = error.Serialize(),
            Type = GetType(error.Type),
        };
        
        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    private static int GetStatusCode(ErrorType type)
    {
        return type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    private static string GetTitle(ErrorType type)
    {
        return type switch
        {
            ErrorType.Validation => "Validation",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            _ => "Server Failure"
        };
    }

    private static string GetType(ErrorType type)
    {
        return type switch
        {
            ErrorType.Validation => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.8",
            _ => "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1"
        };
    }
}