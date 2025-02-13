using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.DomainErrors;
using Shared.Results;

namespace Pharmacy.Domain.ValueObjects;

public class Money : ValueObject
{
    private Money() { }
    private Money(decimal amount)
    {
        Amount = amount;
    }
    public decimal Amount { get; }

    public static Result<Money> Create(decimal amount)
    {
        if (amount < 0)
        {
            return MoneyErrors.NegativeMoneyAmount;
        }

        if (!CheckDigitsCorrectness(amount))
        {
            return MoneyErrors.InvalidMoney;
        }
        
        return new Money(amount);
    }

    private static bool CheckDigitsCorrectness(decimal amount)
    {
        int numOfDigits = 2;

        int multiplier = 1;

        for (int i = 0; i < numOfDigits; i++)
            multiplier *= 10;

        decimal multAmount = amount * multiplier;
        
        return (int)multAmount == Math.Ceiling(multAmount);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
    }
    
    // public static Money operator +(Money a, Money b) => Create(a.Amount + b.Amount);
    // public static Money operator -(Money a, Money b) => Create(a.Amount - b.Amount);
    // public static Money operator *(Money a, int b) => Create(a.Amount * b);
    
    public static bool operator > (Money a, Money b) => a.Amount > b.Amount;
    public static bool operator < (Money a, Money b) => a.Amount < b.Amount;
    public static bool operator >= (Money a, Money b) => a.Amount >= b.Amount;
    public static bool operator <= (Money a, Money b) => a.Amount <= b.Amount;
}