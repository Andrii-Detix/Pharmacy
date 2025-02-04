using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class UserErrors
{
    public static Error EmptyId => Error.Validation("User.Id.Empty", "The user id is empty");
    
    public static Error InvalidName => Error.Validation("User.Name.Invalid", "The user name is invalid");
    
    public static Error SameName => Error.Conflict("User.Name.Same", "The user name is same");
}