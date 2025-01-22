using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Commands.UserCommands.Create;

public class CreateUserHandler(IPharmacyDbContext _context) : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = User.Create(Guid.NewGuid(), command.Name, command.Email, command.Password);

        bool isExist = await _context.Users.AnyAsync(x => x.Email == user.Email, cancellationToken);
        
        if (isExist)
        {
            throw new ArgumentException($"User with email {user.Email} already exists.");
        }
        
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}