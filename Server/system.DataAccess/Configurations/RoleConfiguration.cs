using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace CRMSystem.DataAccess.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    void IEntityTypeConfiguration<RoleEntity>.Configure(EntityTypeBuilder<RoleEntity> builder)
    {

        builder.ToTable("roles");

        builder.HasKey(x => x.Id);

        builder.Property(r => r.Name)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasData(
            new RoleEntity
            {
                Id = (int)RoleEnum.Manager,
                Name = "Менеджер"
            },
            new RoleEntity
            {
                Id = (int)RoleEnum.Client,
                Name = "Клиент"
            },
            new RoleEntity
            {
                Id = (int)RoleEnum.Worker,
                Name = "Работник"
            });

    }
}
