using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.CartQueries.GetById;

public class GetCartByIdHandler(IPharmacyDbContext context) : IQueryHandler<GetCartByIdQuery, Result<Cart>>
{
    public async Task<Result<Cart>> Handle(GetCartByIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.AsNoTracking()
            .Where(c => c.Id == request.Id)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound;
        }
        
        return cart;
    }
}