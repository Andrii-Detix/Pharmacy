using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.Entities;

public class Cart : Entity
{
    private readonly List<CartItem> _items = new();
    
    private Cart() {}
    private Cart(Guid id, Guid userId) : base(id)
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

    public CartItem AddItem(Product product, int quantity)
    {
        if (_items.Any(ci => ci.ProductId == product.Id))
        {
            throw new ArgumentException("Item already exists");
        }

        if (!product.IsAvailableQuantity(quantity))
        {
            throw new Exception($"Requested quantity is not available");
        }
        
        var item = CartItem.Create(Guid.NewGuid(), Id, product.Id, quantity);
        _items.Add(item);
        
        return item;
    }

    public void RemoveItem(Guid id)
    {
        var item = _items.FirstOrDefault(ci => ci.Id == id);
        
        if(item is not null) _items.Remove(item);
    }

    public void RemoveItemByProductId(Guid productId)
    {
        var item = _items.FirstOrDefault(ci => ci.ProductId == productId);
        
        if (item is not null) _items.Remove(item);
    }

    public void ChangeItemQuantity(Product product, int quantity)
    {
        if (quantity == 0)
        {
            RemoveItemByProductId(product.Id);
            return;
        }

        if (!product.IsAvailableQuantity(quantity))
        {
            throw new Exception($"Requested quantity is not available");
        }
        
        var item = _items.FirstOrDefault(ci => ci.ProductId == product.Id);
        
        item?.ChangeQuantity(quantity);
    }

    public void Clear()
    {
        _items.Clear();
    }
}