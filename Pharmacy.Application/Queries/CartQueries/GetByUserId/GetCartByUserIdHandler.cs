using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.CartQueries.GetByUserId;

public class GetCartByUserIdHandler(IPharmacyDbContext _context) : IQueryHandler<GetCartByUserIdQuery, Cart>
{
    public async Task<Cart> Handle(GetCartByUserIdQuery query, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.AsNoTracking()
            .Where(c => c.UserId == query.UserId)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            throw new Exception($"User`s cart not found");
        }

        return cart;
    }
}