using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.ProductQueries.GetByNamePart;

public class GetProductsByNamePartHandler(IPharmacyDbContext _context) : IQueryHandler<GetProductsByNamePartQuery, List<Product>>
{
    public async Task<List<Product>> Handle(GetProductsByNamePartQuery query, CancellationToken cancellationToken)
    {
        if (String.IsNullOrEmpty(query.NamePart))
        {
            throw new ArgumentException("Name part is empty");
        }
        
        List<Product> products = await _context.Products
            .Where(p => p.Name.Contains(query.NamePart))
            .ToListAsync(cancellationToken);

        return products;
    }
}