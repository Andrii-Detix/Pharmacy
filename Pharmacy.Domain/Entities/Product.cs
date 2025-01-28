using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.ValueObjects;

namespace Pharmacy.Domain.Entities;

public class Product : Entity
{
    public const int MaxNameLength = 50;
    
    private Product(Guid id, string name, Money price, int quantity) : base(id)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    
    public string Name { get; }
    public Money Price { get; }
    public int Quantity { get; }

    public static Product Create(Guid id, string name, decimal price, int quantity)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid id");
        }

        name = name.Trim();

        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
        {
            throw new ArgumentException("Invalid name");
        }

        if (quantity < 0)
        {
            throw new ArgumentException("Invalid quantity");
        }

        var priceInstance = Money.Create(price);
        
        return new Product(id, name, priceInstance, quantity);
    }

    public bool IsAvailableQuantity(int requested)
    {
        return requested <= Quantity && requested > 0;
    }
}