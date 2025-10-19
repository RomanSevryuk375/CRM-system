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

        builder.Property(u => u.Id)
            .HasColumnName("user_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.RoleId)
            .HasColumnName("user_role_id")
            .IsRequired();

        builder.Property(u => u.Login)
            .HasColumnName("user_login")
            .HasMaxLength(User.MAX_LOGIN_LENGTH)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .HasColumnName ("user_password_hash")
            .HasMaxLength (User.MAX_PASSWORD_LENGTH)
            .IsRequired();

        builder.HasOne(r => r.Role)
            .WithMany(u => u.Users)
            .HasForeignKey(r => r.RoleId);
    }
}
