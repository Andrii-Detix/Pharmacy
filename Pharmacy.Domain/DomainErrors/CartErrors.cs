using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class CartErrors
{
    public static Error EmptyId => Error.Validation("Cart.EmptyId", "Cart id is empty");
    
    public static Error EmptyUserId => Error.Validation("Cart.EmptyUserId", "User id is empty");
    
    public static Error ItemAlreadyExists => Error.Conflict("Cart.ItemAlreadyExists", "Item already exists");
    
    public static Error InvalidProductQuantity => Error.Validation("Cart.InvalidProductQuantity", "Product quantity is invalid");
    
    public static Error ItemNotFound => Error.NotFound("Cart.ItemNotFound", "Item not found");
}