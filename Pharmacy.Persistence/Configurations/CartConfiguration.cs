using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Persistence.Configurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("cart");
        
        builder.HasKey(c => c.Id)
            .HasName("cart_id");
        
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        builder.Property(c => c.UserId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("user_id");

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<Cart>(c => c.UserId);

        builder.HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(c => c.CartId);
    }
}