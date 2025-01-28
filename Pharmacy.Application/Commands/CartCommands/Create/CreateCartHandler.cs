using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Commands.CartCommands.Create;

public class CreateCartHandler(IPharmacyDbContext _context) : ICommandHandler<CreateCartCommand, Guid>
{
    public async Task<Guid> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var cart = Cart.Create(Guid.NewGuid(), command.UserId);
        
        await _context.Carts.AddAsync(cart, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return cart.Id;
    }
}