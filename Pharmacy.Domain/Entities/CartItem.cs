using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.DomainErrors;
using Shared.Results;

namespace Pharmacy.Domain.Entities;

public class CartItem : Entity
{
    private CartItem() { }
    private CartItem(Guid id, Guid cartId, Guid productId, int quantity) : base(id)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }
    
    public Guid CartId { get; }
    public Guid ProductId { get; }
    public int Quantity { get; private set; }

    public static Result<CartItem> Create(Guid id, Guid cartId, Guid productId, int quantity)
    {
        if (id == Guid.Empty)
        {
            return CartItemErrors.EmptyId;
        }

        if (cartId == Guid.Empty)
        {
            return CartItemErrors.EmptyCartId;
        }

        if (productId == Guid.Empty)
        {
            return CartItemErrors.EmptyProductId;
        }

        if (!IsValidQuantity(quantity))
        {
            return CartItemErrors.InvalidQuantity;
        }
        
        return new CartItem(id, cartId, productId, quantity);
    }

    private static bool IsValidQuantity(int quantity)
    {
        return quantity > 0;
    }

    public Result ChangeQuantity(int quantity)
    {
        if (!IsValidQuantity(quantity))
        {
            return CartItemErrors.InvalidQuantity;
        }
        
        Quantity = quantity;

        return Result.CreateSuccess();
    }
}