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
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        builder.Property(u => u.Name)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("name");

        builder.ComplexProperty(u => u.Email, b =>
        {
            b.IsRequired();
            b.Property(e => e.Value)
                .HasMaxLength(100)
                .HasColumnName("email");
        });

        builder.ComplexProperty(u => u.Password, b =>
        {
            b.IsRequired();
            b.Property(p => p.Value)
                .HasMaxLength(100)
                .HasColumnName("password");
        });
    }
}