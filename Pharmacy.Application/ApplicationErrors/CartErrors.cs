using Shared.Errors;

namespace Pharmacy.Application.ApplicationErrors;

public static class CartErrors
{
    public static Error AlreadyExists => Error.Conflict("Cart.AlreadyExists", "Cart already exists");
    
    public static Error NotFound => Error.NotFound("Cart.NotFound", "Cart not found");
}