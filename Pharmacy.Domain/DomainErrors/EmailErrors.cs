using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class EmailErrors
{
    public static Error EmptyEmail => Error.Validation("Email.Empty", "Email is empty");
    
    public static Error InvalidEmail => Error.Validation("Email.Invalid", "Email is invalid");
}