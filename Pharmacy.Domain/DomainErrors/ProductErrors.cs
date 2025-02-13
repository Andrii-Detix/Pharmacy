using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class ProductErrors
{
    public static Error EmptyId => Error.Validation("Product.EmptyId", "The product id is empty");
    
    public static Error EmptyName => Error.Validation("Product.EmptyName", "The product name is empty");
    
    public static Error LongName(int maxLength) => 
        Error.Validation("Product.LongName", $"The product name is longer than {maxLength} characters");
    
    public static Error InvalidQuantity => 
        Error.Validation("Product.InvalidQuantity", "The product quantity is invalid");
}