using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.DomainErrors;
using Shared.Results;

namespace Pharmacy.Domain.ValueObjects;

public class Email : ValueObject
{
    private Email() { }
    private Email(string email)
    {
        Value = email;
    }
    
    public string Value { get; }

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return EmailErrors.EmptyEmail;
        }

        email = email.Trim();
        var emailChar = '@';
        
        int index = email.IndexOf(emailChar);
        bool isValidEmail = index != -1 && index !=0 && index != email.Length -1;

        if (!isValidEmail)
        {
            return EmailErrors.InvalidEmail;
        }

        return new Email(email);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}