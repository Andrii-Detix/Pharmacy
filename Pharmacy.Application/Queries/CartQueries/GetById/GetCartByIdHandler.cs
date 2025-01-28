using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.CartQueries.GetById;

public class GetCartByIdHandler(IPharmacyDbContext _context) : IQueryHandler<GetCartByIdQuery, Cart>
{
    public async Task<Cart> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _context.Carts.AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            throw new Exception($"Cart with id: {request.Id} not found");
        }
        
        return cart;
    }
}