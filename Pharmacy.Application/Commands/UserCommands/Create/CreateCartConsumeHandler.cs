using MediatR;
using Pharmacy.Application.Abstractions;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Commands.UserCommands.Create;

public class CreateCartConsumeHandler(IPharmacyDbContext context) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        Result<Cart> cartResult = Cart.Create(Guid.NewGuid(), notification.Id);
        
        if (cartResult.IsFailure)
        {
            return;
        }
        
        Cart cart = cartResult.Value!;
        
        await context.Carts.AddAsync(cart, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}