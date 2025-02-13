using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Application.ApplicationErrors;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.RemoveItem;

public class RemoveCartItemHandler(IPharmacyDbContext context) : ICommandHandler<RemoveCartItemCommand, Result>
{
    public async Task<Result> Handle(RemoveCartItemCommand command, CancellationToken cancellationToken)
    {
        var cart = await context.Carts
            .Where(c => c.Id == command.CartId)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound;
        }
        
        cart.RemoveItem(command.ItemId);
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.CreateSuccess();
    }
}