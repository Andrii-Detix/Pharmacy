using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Application.ApplicationErrors;
using Shared.Results;

namespace Pharmacy.Application.Commands.CartCommands.Clear;

public class ClearCartHandler(IPharmacyDbContext _context) : ICommandHandler<ClearCartCommand, Result>
{
    public async Task<Result> Handle(ClearCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts
            .Include(c => c.Items)
            .Where(c => c.Id == command.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound;
        }
        
        cart.Clear();
        
        await _context.SaveChangesAsync(cancellationToken);

        return Result.CreateSuccess();
    }
}