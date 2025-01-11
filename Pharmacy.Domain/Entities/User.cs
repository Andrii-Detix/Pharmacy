using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.Entities;

public class User : Entity
{
    public const int MaxNameLength = 30;
    public const int MinPasswordLength = 8;
    
    private User(Guid id, string name, string email, string password) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }

    public static User Create(Guid id, string name, string email, string password)
    {
        if(id == Guid.Empty)
        {
            throw new Exception($"Invalid {nameof(id)}");
        }

        if (String.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength)
        {
            throw new Exception($"Invalid {nameof(name)}");
        }

        if (String.IsNullOrWhiteSpace(password) || password.Length < MaxNameLength)
        {
            throw new Exception($"Invalid {nameof(password)}");
        }

        if (!CheckEmailValidity(email))
        {
            throw new Exception($"Invalid {nameof(email)}");
        }
        
        return new User(id, name, email, password);
    }

    private static bool CheckEmailValidity(string email)
    {
        char emailAttribute = '@';
        
        int index = email.IndexOf(emailAttribute);
        
        return index != -1 && index != 0 && index < email.Length - 1;
    }
}