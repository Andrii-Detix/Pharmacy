using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Commands.CartCommands.ChangeCartItemQuantity;

public class ChangeCartItemQuantityHandler(IPharmacyDbContext _context) : ICommandHandler<ChangeCartItemQuantityCommand, Cart>
{
    public async Task<Cart> Handle(ChangeCartItemQuantityCommand command, CancellationToken cancellationToken)
    {
        var product = await _context.Products.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            throw new Exception($"Product with id {command.ProductId} not found");
        }

        var cart = await _context.Carts
            .Include(c => c.Items)
            .Where(c => c.Id == command.CartId)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            throw new Exception($"Cart with id {command.CartId} not found");
        }
        
        cart.ChangeItemQuantity(product, command.Quantity);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return cart;
    }
}