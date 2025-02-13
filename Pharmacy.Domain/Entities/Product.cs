using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.DomainErrors;
using Pharmacy.Domain.ValueObjects;
using Shared.Results;

namespace Pharmacy.Domain.Entities;

public class Product : Entity
{
    public const int MaxNameLength = 50;
    
    private Product() { }
    private Product(Guid id, string name, Money price, int quantity) : base(id)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
    
    public string Name { get; }
    public Money Price { get; }
    public int Quantity { get; }

    public static Result<Product> Create(Guid id, string name, decimal price, int quantity)
    {
        if (id == Guid.Empty)
        {
            return ProductErrors.EmptyId;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return ProductErrors.EmptyName;
        }
        
        name = name.Trim();

        if (name.Length > MaxNameLength)
        {
            return ProductErrors.LongName(MaxNameLength);
        }

        if (quantity < 0)
        {
            return ProductErrors.InvalidQuantity;
        }

        var priceResult = Money.Create(price);
        if (priceResult.IsFailure)
        {
            return priceResult.Error!;
        }
        
        return new Product(id, name, priceResult.Value!, quantity);
    }

    public bool IsAvailableQuantity(int requested)
    {
        return requested <= Quantity && requested > 0;
    }
}