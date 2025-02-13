using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.Create;

public class CreateCartHandler(IPharmacyDbContext context) : ICommandHandler<CreateCartCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        bool isExist = await context.Carts.AnyAsync(c => c.UserId == command.UserId, cancellationToken);

        if (isExist)
        {
            return CartErrors.AlreadyExists;
        }
        
        Result<Cart> cartResult = Cart.Create(Guid.NewGuid(), command.UserId);
        
        if (cartResult.IsFailure)
        {
            return cartResult.Error;
        }
        
        Cart cart = cartResult.Value!;
        
        await context.Carts.AddAsync(cart, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return cart.Id;
    }
}