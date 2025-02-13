using Shared.Errors;

namespace Pharmacy.Application.ApplicationErrors;

public static class UserErrors
{
    public static Error AlreadyExists => Error.Conflict("User.AlreadyExists", "User already exists"); 
    
    public static Error NotFound => Error.NotFound("User.NotFound", "User not found");
}