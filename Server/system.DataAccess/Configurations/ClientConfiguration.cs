using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    void IEntityTypeConfiguration<ClientEntity>.Configure(EntityTypeBuilder<ClientEntity> builder)
    {

        builder.ToTable("clients");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(c => c.Surname)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(c => c.PhoneNumber)
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasMaxLength(128)
            .IsRequired(false);

        builder.HasOne(c => c.User)
           .WithOne(u => u.Client)
           .HasForeignKey<ClientEntity>(c => c.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.PhoneNumber)
            .IsUnique();

        builder.HasIndex(c => c.Email)
            .IsUnique();

    }
}
