using System.Text.RegularExpressions;
using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.DomainErrors;
using Shared.Results;

namespace Pharmacy.Domain.ValueObjects;

public class Password : ValueObject
{
    public const int MinLength = 8;
    
    private Password() { }
    private Password(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Result<Password> Create(string password)
    {
        if (String.IsNullOrWhiteSpace(password))
        {
            return PasswordErrors.EmptyPassword;
        }
        
        password = password.Trim();

        if (password.Length < MinLength)
        {
            return PasswordErrors.InvalidPassword($"Password must have at least {MinLength} characters");
        }

        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            return PasswordErrors.InvalidPassword("Password must consist at least one number");
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            return PasswordErrors.InvalidPassword("Password must consist at least one small letter");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            return PasswordErrors.InvalidPassword($"{nameof(password)} must consist at least one capital letter");
        }
        
        return new Password(password);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}