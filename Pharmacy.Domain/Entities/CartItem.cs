using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.Entities;

public class CartItem : Entity
{
    private CartItem(Guid id, Guid cartId, Guid productId, int quantity) : base(id)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }
    
    public Guid CartId { get; }
    public Guid ProductId { get; }
    public int Quantity { get; private set; }

    public static CartItem Create(Guid id, Guid cartId, Guid productId, int quantity)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid id");
        }

        if (cartId == Guid.Empty)
        {
            throw new ArgumentException("Invalid cart id");
        }

        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Invalid product id");
        }

        if (!IsValidQuantity(quantity))
        {
            throw new ArgumentException("Invalid quantity");
        }
        
        return new CartItem(id, cartId, productId, quantity);
    }

    private static bool IsValidQuantity(int quantity)
    {
        return quantity > 0;
    }

    public void ChangeQuantity(int quantity)
    {
        if (!IsValidQuantity(quantity))
        {
            throw new ArgumentException("Invalid quantity");
        }
        
        Quantity = quantity;
    }
}