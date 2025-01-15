using System.Text.RegularExpressions;
using Pharmacy.Domain.Abstractions.Models;

namespace Pharmacy.Domain.ValueObjects;

public class Password : ValueObject
{
    public const int MinLength = 8;
    
    private Password(string value)
    {
        Value = value;
    }
    public string Value { get; }

    public static Password Create(string password)
    {
        if (String.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }
        
        password = password.Trim();

        if (password.Length < MinLength)
        {
            throw new ArgumentException($"{nameof(password)} must have at least {MinLength} characters");
        }

        if (!Regex.IsMatch(password, @"[0-9]"))
        {
            throw new ArgumentException($"{nameof(password)} must consist at least one number");
        }

        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            throw new ArgumentException($"{nameof(password)} must consist at least one small letter");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            throw new ArgumentException($"{nameof(password)} must consist at least one capital letter");
        }
        
        return new Password(password);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}