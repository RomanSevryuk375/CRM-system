using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class WorkConfiguration : IEntityTypeConfiguration<WorkEntity>
{
    void IEntityTypeConfiguration<WorkEntity>.Configure(EntityTypeBuilder<WorkEntity> builder)
    {

        builder.ToTable("works_catalog");

        builder.HasKey(x => x.Id);

        builder.Property(w => w.Title)
            .HasMaxLength(ValidationConstants.MAX_NAME_LENGTH)
            .IsRequired();

        builder.Property(w => w.Category)
            .HasMaxLength(ValidationConstants.MAX_CATEGORY_LENGTH)
            .IsRequired();

        builder.Property(w => w.Description)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired();

        builder.Property(w => w.StandardTime)
            .HasColumnType("decimal(4, 2)")
            .IsRequired();

    }
}
