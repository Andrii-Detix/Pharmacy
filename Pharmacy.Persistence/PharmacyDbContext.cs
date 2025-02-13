using Microsoft.EntityFrameworkCore;
using Pharmacy.Application.Abstractions;
using Pharmacy.Domain.Entities;
using Pharmacy.Persistence.Configurations;

namespace Pharmacy.Persistence;

public class PharmacyDbContext : DbContext, IPharmacyDbContext
{
    public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CartConfiguration());
        modelBuilder.ApplyConfiguration(new CartItemConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
    
}