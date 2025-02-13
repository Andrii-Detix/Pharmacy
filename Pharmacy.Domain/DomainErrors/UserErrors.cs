using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class UserErrors
{
    public static Error EmptyId => Error.Validation("User.EmptyId", "The user id is empty");
    
    public static Error InvalidName => Error.Validation("User.InvalidName", "The user name is invalid");
    
    public static Error SameName => Error.Conflict("User.SameName", "The user name is same");
}