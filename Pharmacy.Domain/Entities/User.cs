using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.ValueObjects;

namespace Pharmacy.Domain.Entities;

public class User : Entity
{
    public const int MaxNameLength = 30;
    
    private User(Guid id, string name, Email email, Password password) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    
    public string Name { get; private set; }
    public Email Email { get; }
    public Password Password { get; }

    public static User Create(Guid id, string name, Email email, Password password)
    {
        if(id == Guid.Empty)
        {
            throw new Exception($"Invalid {nameof(id)}");
        }

        name = name.Trim();
        
        if (!CheckNameValidity(name))
        {
            throw new Exception($"Invalid {nameof(name)}");
        }
        
        return new User(id, name, email, password);
    }

    public void UpdateName(string name)
    {
        if (!CheckNameValidity(name))
        {
            throw new ArgumentException($"Invalid {nameof(name)}");
        }
        
        Name = name;
    }

    private static bool CheckNameValidity(string name)
    {
        return String.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength;
    }
}