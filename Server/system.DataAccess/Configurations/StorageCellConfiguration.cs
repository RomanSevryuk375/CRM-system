using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class StorageCellConfiguration : IEntityTypeConfiguration<StorageCellEntity>
{
    void IEntityTypeConfiguration<StorageCellEntity>.Configure(EntityTypeBuilder<StorageCellEntity> builder)
    {

        builder.ToTable("storage_cells");

        builder.HasKey(x => x.Id);

        builder.Property(sc => sc.Rack)
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(sc => sc.Shelf)
            .HasMaxLength(16)
            .IsRequired();

    }
}
