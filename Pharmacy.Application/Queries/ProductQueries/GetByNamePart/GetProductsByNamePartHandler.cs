using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Queries.ProductQueries.GetByNamePart;

public class GetProductsByNamePartHandler(IPharmacyDbContext context) : IQueryHandler<GetProductsByNamePartQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetProductsByNamePartQuery query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(query.NamePart))
        {
            return ProductErrors.EmptyNamePart;
        }
        
        List<Product> products = await context.Products
            .Where(p => p.Name.Contains(query.NamePart))
            .ToListAsync(cancellationToken);

        return products;
    }
}