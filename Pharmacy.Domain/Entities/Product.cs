using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.Entities;

public class Product : Entity
{
    public const int MaxNameLength = 50;
    
    private Product(Guid id, string name, decimal price, int quantity) : base(id)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    
    public string Name { get; }
    public decimal Price { get; }
    public int Quantity { get; }

    public static Product Create(Guid id, string name, decimal price, int quantity)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Invalid id");
        }

        if (string.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
        {
            throw new ArgumentException("Invalid name");
        }

        if (price <= 0)
        {
            throw new ArgumentException("Invalid price");
        }

        if (quantity < 0)
        {
            throw new ArgumentException("Invalid quantity");
        }
        
        return new Product(id, name, price, quantity);
    }
}