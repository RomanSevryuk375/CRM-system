using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class ShiftConfiguration : IEntityTypeConfiguration<ShiftEntity>
{
    void IEntityTypeConfiguration<ShiftEntity>.Configure(EntityTypeBuilder<ShiftEntity> builder)
    {

        builder.ToTable("shifts");

        builder.HasKey(x => x.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(ValidationConstants.MAX_TYPE_NAME)
            .IsRequired();

        builder.Property(s => s.StartAt)
            .IsRequired();

        builder.Property(s => s.EndAt)
            .IsRequired();

    }
}
