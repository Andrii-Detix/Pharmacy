using Pharmacy.Domain.Abstractions.Models;
using Pharmacy.Domain.DomainErrors;
using Pharmacy.Domain.ValueObjects;
using Shared.Results;

namespace Pharmacy.Domain.Entities;

public class User : Entity
{
    public const int MaxNameLength = 30;
    
    private User() { }
    private User(Guid id, string name, Email email, Password password) : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    
    public string Name { get; private set; }
    public Email Email { get; }
    public Password Password { get; }

    public static Result<User> Create(Guid id, string name, string email, string password)
    {
        if(id == Guid.Empty)
        {
            return UserErrors.EmptyId;
        }

        name = name.Trim();
        
        if (!CheckNameValidity(name))
        {
            return UserErrors.InvalidName;
        }
        
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
        {
            return emailResult.Error!;
        }
        
        var passwordResult = Password.Create(password);
        if (passwordResult.IsFailure)
        {
            return passwordResult.Error!;
        }
        
        return new User(id, name, emailResult.Value!, passwordResult.Value!);
    }

    public Result UpdateName(string name)
    {
        if (!CheckNameValidity(name))
        {
            return UserErrors.InvalidName;
        }

        if (Name == name)
        {
            return UserErrors.SameName;
        }
        
        Name = name;

        return Result.CreateSuccess();
    }

    private static bool CheckNameValidity(string name)
    {
        return !String.IsNullOrWhiteSpace(name) && name.Length <= MaxNameLength;
    }
}