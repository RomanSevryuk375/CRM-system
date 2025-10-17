using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using system.Core.Models;
using system.DataAccess.Entites;

namespace system.DataAccess.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<ClientEntity>
{
    void IEntityTypeConfiguration<ClientEntity>.Configure(EntityTypeBuilder<ClientEntity> builder)
    {
        builder.ToTable("clients");

        builder.HasKey(x => x.ClientId);

        builder.Property(c => c.ClientId)
            .HasColumnName("client_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(c => c.ClientUserId)
            .HasColumnName("client_user_id")
            .IsRequired();

        builder.Property(c => c.ClientName)
            .HasColumnName("client_name")
            .HasMaxLength(Client.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(c => c.ClientSurname)
            .HasColumnName("client_surname")
            .HasMaxLength(Client.MAX_SURNAME_LENGTH)
            .IsRequired();

        builder.Property(c => c.ClientPhoneNumber)
            .HasColumnName("client_phone_number")
            .HasMaxLength(Client.MAX_PHONE_NUMBER_LENGTH)
            .IsRequired();

        builder.Property(c => c.ClientEmail)
            .HasColumnName("client_email")
            .HasMaxLength(Client.MAX_EMAIL_LENGTH)
            .IsRequired();

        builder.HasIndex(c => c.ClientEmail)
           .IsUnique();

        builder.HasIndex(c => c.ClientPhoneNumber)
            .IsUnique();

        builder.HasOne<UserEntity>()
           .WithMany()
           .HasForeignKey(c => c.ClientUserId)
           .OnDelete(DeleteBehavior.Cascade);
    }
}
