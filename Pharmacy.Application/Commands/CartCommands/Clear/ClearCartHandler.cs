using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;

namespace Pharmacy.Application.Commands.CartCommands.Clear;

public class ClearCartHandler(IPharmacyDbContext _context) : ICommandHandler<ClearCartCommand>
{
    public async Task Handle(ClearCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .Where(c => c.Id == command.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            throw new Exception($"Cart with id {command.Id} not found");
        }
        
        cart.Clear();
        
        await _context.SaveChangesAsync(cancellationToken);
    }
}