using Shared.Errors;

namespace Pharmacy.Application.ApplicationErrors;

public static class ProductErrors
{
    public static Error AlreadyExists => Error.Conflict("Product.AlreadyExists", "Product already exists");
    
    public static Error NotFound => Error.NotFound("Product.NotFound", "Product not found");
    
    public static Error EmptyNamePart => Error.Validation("Product.EmptyNamePart", "Request product name part is empty");
}