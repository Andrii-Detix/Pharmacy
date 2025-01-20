﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.ValueObjects;

namespace Pharmacy.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        
        builder.HasKey(p => p.Id)
            .HasName("pk_product");

        builder.Property(p => p.Id)
            .HasColumnName("id");
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Product.MaxNameLength)
            .HasColumnName("name");
        
        builder.Property(p => p.Price)
            .HasConversion(pr => pr.Amount,
                amount => Money.Create(amount))
            .IsRequired()
            .HasColumnName("price");
        
        builder.Property(p => p.Quantity)
            .IsRequired()
            .HasColumnName("quantity");
    }
}