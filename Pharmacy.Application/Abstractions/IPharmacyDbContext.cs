using Microsoft.EntityFrameworkCore;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Application.Abstractions;

public interface IPharmacyDbContext
{
    DbSet<User> Users { get; }

    DbSet<Product> Products { get; }

    DbSet<Cart> Carts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}