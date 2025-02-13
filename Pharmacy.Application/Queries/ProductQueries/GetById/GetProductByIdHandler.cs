using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.ProductQueries.GetById;

public class GetProductByIdHandler(IPharmacyDbContext context) : IQueryHandler<GetProductByIdQuery, Result<Product>>
{
    public async Task<Result<Product>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await context.Products.AsNoTracking()
            .Where(p => p.Id == query.Id)
            .FirstOrDefaultAsync(cancellationToken);

        return product is null ? ProductErrors.NotFound : product;
    }
}