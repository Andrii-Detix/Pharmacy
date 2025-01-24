using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Queries;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Queries.ProductQueries.GetById;

public class GetProductByIdHandler(IPharmacyDbContext _context) : IQueryHandler<GetProductByIdQuery, Product>
{
    public async Task<Product> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(query.Id);

        if (product is null)
        {
            throw new Exception($"Product with id {query.Id} not found");
        }
        
        return product;
    }
}