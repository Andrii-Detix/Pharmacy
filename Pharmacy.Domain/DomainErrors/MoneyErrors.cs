using Shared.Errors;

namespace Pharmacy.Domain.DomainErrors;

public static class MoneyErrors
{
    public static Error NegativeMoneyAmount => Error.Validation("Money.NegativeAmount", "Amount of money cannot be negative");
    
    public static Error InvalidMoney => Error.Validation("Money.Invalid", "Money is invalid");
}