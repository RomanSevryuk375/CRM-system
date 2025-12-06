using CRMSystem.Core.Models;
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

        builder.Property(c => c.Id)
            .HasColumnName("client_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(c => c.UserId)
            .HasColumnName("client_user_id")
            .IsRequired();

        builder.Property(c => c.Name)
            .HasColumnName("client_name")
            .HasMaxLength(Client.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(c => c.Surname)
            .HasColumnName("client_surname")
            .HasMaxLength(Client.MAX_SURNAME_LENGTH)
            .IsRequired();

        builder.Property(c => c.PhoneNumber)
            .HasColumnName("client_phone_number")
            .HasMaxLength(Client.MAX_PHONE_NUMBER_LENGTH)
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnName("client_email")
            .HasMaxLength(Client.MAX_EMAIL_LENGTH)
            .IsRequired(false);

        builder.HasOne(c => c.User)
           .WithOne(u => u.Client)
           .HasForeignKey<ClientEntity>(c => c.UserId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
