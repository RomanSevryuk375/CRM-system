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

        builder.HasKey(x => x.client_id);

        builder.Property(c => c.client_id)
            .HasColumnName("client_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(c => c.client_user_id)
            .HasColumnName("client_user_id")
            .IsRequired();

        builder.Property(c => c.client_name)
            .HasColumnName("client_name")
            .HasMaxLength(Client.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(c => c.client_surname)
            .HasColumnName("client_surname")
            .HasMaxLength(Client.MAX_SURNAME_LENGTH)
            .IsRequired();

        builder.Property(c => c.client_phone_number)
            .HasColumnName("client_phone_number")
            .HasMaxLength(Client.MAX_PHONE_NUMBER_LENGTH)
            .IsRequired();

        builder.Property(c => c.client_email)
            .HasColumnName("client_email")
            .HasMaxLength(Client.MAX_EMAIL_LENGTH)
            .IsRequired();

        builder.HasOne(c => c.User)
            .WithOne(u => u.Client)                      // User имеет одного Client
            .HasForeignKey<ClientEntity>(c => c.client_user_id);
    }
}
