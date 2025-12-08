using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class PartConfiguration : IEntityTypeConfiguration<PartEntity>
{
    void IEntityTypeConfiguration<PartEntity>.Configure(EntityTypeBuilder<PartEntity> builder)
    {

        builder.ToTable("parts");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.CategoryId)
            .IsRequired();

        builder.Property(p => p.OEMArticle)
            .HasMaxLength(64)
            .IsRequired(false);

        builder.Property(p => p.ManufacturerArticle)
            .HasMaxLength(64)
            .IsRequired(false);

        builder.Property(p => p.InternalArticle)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(512)
            .IsRequired(false);

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.Manufacturer)
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(x => x.Applicability)
            .HasMaxLength(128)
            .IsRequired();

        builder.HasOne(p => p.PartCategory)
            .WithMany(pc => pc.Parts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
