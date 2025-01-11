namespace Pharmacy.Domain.Abstractions.Models;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
    }
    
    public Guid Id { get; }
}