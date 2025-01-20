using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pharmacy.Domain.Entities;
using Pharmacy.Domain.ValueObjects;

namespace Pharmacy.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        
        builder.HasKey(u => u.Id)
            .HasName("pk_user");

        builder.Property(u => u.Id)
            .HasColumnName("id");
        
        builder.Property(u => u.Name)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("name");
        
        builder.Property(u => u.Email)
            .HasConversion(u => u.Value,
                value => Email.Create(value))
            .IsRequired()
            .HasColumnName("email");
        
        builder.Property(u => u.Password)
            .HasConversion(u => u.Value,
                value => Password.Create(value))
            .IsRequired()
            .HasColumnName("password");
    }
}