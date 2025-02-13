using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.AddCartItem;

public class AddCartItemHandler(IPharmacyDbContext context) : ICommandHandler<AddCartItemCommand, Result<Cart>>
{
    public async Task<Result<Cart>> Handle(AddCartItemCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound;
        }

        var cart = await context.Carts
            .Include(c => c.Items)
            .Where(c => c.Id == command.CartId)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound;
        }
        
        Result result = cart.AddItem(product, command.Quantity);

        if (result.IsFailure)
        {
            return result.Error;
        }
        
        await context.SaveChangesAsync(cancellationToken);

        return cart;
    }
}