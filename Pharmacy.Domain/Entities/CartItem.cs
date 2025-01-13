using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.Entities;

public class CartItem : Entity
{
    private CartItem(Guid id, Guid productId, int quantity) : base(id)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid ProductId { get; }
    public int Quantity { get; }

    public static CartItem Create(Guid id, Guid productId, int quantity)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid id");
        }

        if (productId == Guid.Empty)
        {
            throw new ArgumentException("Invalid product id");
        }

        if (quantity <= 0)
        {
            throw new ArgumentException("Invalid quantity");
        }
        
        return new CartItem(id, productId, quantity);
    }
}