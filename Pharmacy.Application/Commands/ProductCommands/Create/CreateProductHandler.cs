using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Application.ApplicationErrors;
using Pharmacy.Domain.Entities;
using Shared.Results;

namespace Pharmacy.Application.Commands.ProductCommands.Create;

public class CreateProductHandler(IPharmacyDbContext context) : ICommandHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        bool isExists = await context.Products.AnyAsync(p => p.Name == command.Name, cancellationToken);

        if (isExists)
        {
            return ProductErrors.AlreadyExists;
        }
        
        Result<Product> productResult = Product.Create(Guid.NewGuid(), command.Name, command.Price, command.Quantity);

        if (productResult.IsFailure)
        {
            return productResult.Error!;
        }
        
        Product product = productResult.Value!;
        
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        return product.Id;
    }
}