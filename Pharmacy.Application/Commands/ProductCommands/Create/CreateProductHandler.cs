using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Application.Abstractions.Commands;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Commands.ProductCommands.Create;

public class CreateProductHandler(IPharmacyDbContext _context) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        bool isExists = await _context.Products.AnyAsync(p => p.Name == command.Name, cancellationToken);

        if (isExists)
        {
            throw new Exception($"Product with name: {command.Name} already exists.");
        }
        
        var product = Product.Create(Guid.NewGuid(), command.Name, command.Price, command.Quantity);
        
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return product.Id;
    }
}