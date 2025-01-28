using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities;

namespace Pharmacy.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("cart_item");
        
        builder.HasKey(ci => ci.Id)
            .HasName("pk_cart_item");

        builder.Property(ci => ci.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        builder.Property(ci => ci.CartId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("cart_id");
        
        builder.Property(ci => ci.ProductId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("product_id");
        
        builder.Property(ci => ci.Quantity)
            .IsRequired()
            .HasColumnName("quantity");

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(ci => ci.ProductId);
    }
}