using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CRMSystem.Core.Models;

namespace CRMSystem.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    void IEntityTypeConfiguration<UserEntity>.Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.UserId);

        builder.Property(u => u.UserId)
            .HasColumnName("user_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.UserRoleId)
            .HasColumnName("user_role_id")
            .IsRequired();

        builder.Property(u => u.UserLogin)
            .HasColumnName("user_login")
            .HasMaxLength(User.MAX_LOGIN_LENGHT)
            .IsRequired();

        builder.Property(u => u.UserPasswordHash)
            .HasColumnName("user_password_hash")
            .HasMaxLength(User.MAX_PASSWORD_LENGHT)
            .IsRequired();

        builder.HasOne<RoleEntity>()
           .WithMany()
           .HasForeignKey(u => u.UserRoleId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
