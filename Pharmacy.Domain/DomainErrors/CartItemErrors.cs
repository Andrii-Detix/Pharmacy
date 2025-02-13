using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class CartItemErrors
{
    public static Error EmptyId => Error.Validation("CartItem.EmptyId", "CartItem id is empty");
    
    public static Error EmptyCartId => Error.Validation("CartItem.EmptyCartId", "Cart id is empty");
    
    public static Error EmptyProductId => Error.Validation("CartItem.EmptyProductId", "Product id is empty");
    
    public static Error InvalidQuantity => Error.Validation("CartItem.InvalidQuantity", "Quantity is invalid");
}