using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<SupplierEntity>
{
    void IEntityTypeConfiguration<SupplierEntity>.Configure(EntityTypeBuilder<SupplierEntity> builder)
    {

        builder.ToTable("suppliers");

        builder.HasKey(x => x.Id);

        builder.Property(su =>su.Name) 
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(su => su.Contacts)
            .HasMaxLength(256)
            .IsRequired();

    }
}
