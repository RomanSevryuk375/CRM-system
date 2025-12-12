using CRMSystem.Core.Constants;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    void IEntityTypeConfiguration<UserEntity>.Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(u => u.RoleId)
            .IsRequired();

        builder.Property(u => u.Login)
            .HasMaxLength(ValidationConstants.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(u => u.Login)
            .IsUnique();
    }
}
