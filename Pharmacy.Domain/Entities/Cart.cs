using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.Entities;

public class Cart : Entity
{
    private List<CartItem> _items = new();
    public Cart(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }
    
    public Guid UserId { get; }
    public IReadOnlyCollection<CartItem> Items => _items;

    public static Cart Create(Guid id, Guid userId)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid id");
        }

        if (userId == Guid.Empty)
        {
            throw new ArgumentException("Invalid user id");
        }
        
        return new Cart(id, userId);
    }

    public CartItem AddItem(Guid productId, int quantity)
    {
        if (_items.Any(i => i.ProductId == productId))
        {
            throw new ArgumentException("Item already exists");
        }
        
        var item = CartItem.Create(Guid.NewGuid(),Id, productId, quantity);
        _items.Add(item);
        
        return item;
    }

    public void RemoveItem(Guid itemId)
    {
        var item = _items.FirstOrDefault(x => x.Id == itemId);
        
        if(item is not null) _items.Remove(item);
    }
}