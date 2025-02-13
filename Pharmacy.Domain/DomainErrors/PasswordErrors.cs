using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class PasswordErrors
{
    public static Error EmptyPassword => Error.Validation("Password.Empty", "Password cannot be empty"); 
    
    public static Error InvalidPassword(string description) => Error.Validation("Password.Invalid", description);
}