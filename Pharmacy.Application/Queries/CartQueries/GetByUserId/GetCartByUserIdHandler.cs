using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.CartQueries.GetByUserId;

public class GetCartByUserIdHandler(IPharmacyDbContext context) : IQueryHandler<GetCartByUserIdQuery, Result<Cart>>
{
    public async Task<Result<Cart>> Handle(GetCartByUserIdQuery query, CancellationToken cancellationToken)
    {
        var cart = await context.Carts.AsNoTracking()
            .Where(c => c.UserId == query.UserId)
            .Include(c => c.Items)
            .FirstOrDefaultAsync(cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound;
        }

        return cart;
    }
}