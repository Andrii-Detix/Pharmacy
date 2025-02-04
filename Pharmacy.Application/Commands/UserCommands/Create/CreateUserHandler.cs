using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Commands.UserCommands.Create;

public class CreateUserHandler(IPharmacyDbContext context) : ICommandHandler<CreateUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        Result<User> userResult = User.Create(Guid.NewGuid(), command.Name, command.Email, command.Password);

        if (userResult.IsFailure)
        {
            return userResult.Error!;
        }
        
        User user = userResult.Value!;
        
        bool isExist = await context.Users.AnyAsync(u => u.Email == user.Email, cancellationToken);
        
        if (isExist)
        {
            return UserErrors.AlreadyExists;
        }
        
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}