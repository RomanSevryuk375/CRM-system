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

        builder.Property(su =>su.Id)
            .HasColumnName("supplier_id")
            .IsRequired();

        builder.Property(su =>su.Name) 
            .HasColumnName ("supplier_name")
            .IsRequired();

        builder.Property(su => su.Contacts)
            .HasColumnName("supplier_contacts")
            .IsRequired();
    }
}
