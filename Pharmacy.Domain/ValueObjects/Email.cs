using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.ValueObjects;

public class Email : ValueObject
{
    private Email(string email)
    {
        Value = email;
    }
    
    public string Value { get; }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentNullException(nameof(email));
        }

        email = email.Trim();
        var emailChar = '@';
        
        int index = email.IndexOf(emailChar);
        bool isValidEmail = index != -1 && index !=0 && index != email.Length -1;

        if (!isValidEmail)
        {
            throw new ArgumentException("Invalid email");
        }

        return new Email(email);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}