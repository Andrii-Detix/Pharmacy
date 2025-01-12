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
    
    public string Name { get; private set; }
    public string Email { get; }
    public string Password { get; }

    public static User Create(Guid id, string name, string email, string password)
    {
        if(id == Guid.Empty)
        {
            throw new Exception($"Invalid {nameof(id)}");
        }

        if (!CheckNameValidity(name))
        {
            throw new Exception($"Invalid {nameof(name)}");
        }

        if (!CheckPasswordValidity(password))
        {
            throw new Exception($"Invalid {nameof(password)}");
        }

        if (!CheckEmailValidity(email))
        {
            throw new Exception($"Invalid {nameof(email)}");
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

    private static bool CheckEmailValidity(string email)
    {
        char emailAttribute = '@';
        
        int index = email.IndexOf(emailAttribute);
        
        return index != -1 && index != 0 && index < email.Length - 1;
    }

    private static bool CheckPasswordValidity(string password)
    {
        return String.IsNullOrWhiteSpace(password) || password.Length < MinPasswordLength;
    }

    private static bool CheckNameValidity(string name)
    {
        return String.IsNullOrWhiteSpace(name) || name.Length > MaxNameLength;
    }
}