using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.DomainErrors;
using Shared.Results;

namespace Pharmacy.Domain.Entities;

public class Cart : Entity
{
    private readonly List<CartItem> _items = [];
    
    private Cart() {}
    private Cart(Guid id, Guid userId) : base(id)
    {
        UserId = userId;
    }
    
    public Guid UserId { get; }
    public IReadOnlyCollection<CartItem> Items => _items;

    public static Result<Cart> Create(Guid id, Guid userId)
    {
        if (id == Guid.Empty)
        {
            return CartErrors.EmptyId;
        }

        if (userId == Guid.Empty)
        {
            return CartErrors.EmptyUserId;
        }
        
        return new Cart(id, userId);
    }

    public Result<CartItem> AddItem(Product product, int quantity)
    {
        if (_items.Any(ci => ci.ProductId == product.Id))
        {
            return CartErrors.ItemAlreadyExists;
        }

        if (!product.IsAvailableQuantity(quantity))
        {
            return CartErrors.InvalidProductQuantity;
        }
        
        Result<CartItem> itemResult = CartItem.Create(Guid.NewGuid(), Id, product.Id, quantity);

        if (itemResult.IsSuccess)
        {
            _items.Add(itemResult.Value!);
        }
        
        return itemResult;
    }

    public void RemoveItem(Guid id)
    {
        var item = _items.FirstOrDefault(ci => ci.Id == id);
        
        if(item is not null) _items.Remove(item);
    }

    public Result ChangeItemQuantity(Product product, int quantity)
    {
        if (!product.IsAvailableQuantity(quantity))
        {
            return CartErrors.InvalidProductQuantity;
        }
        
        var item = _items.FirstOrDefault(ci => ci.ProductId == product.Id);

        if (item is null)
        {
            return CartErrors.ItemNotFound;
        }
        
        Result result = item.ChangeQuantity(quantity);
        
        return result;
    }

    public void Clear()
    {
        _items.Clear();
    }
}