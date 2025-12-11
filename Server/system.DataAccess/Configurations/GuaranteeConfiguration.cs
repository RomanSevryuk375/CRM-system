using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class GuaranteeConfiguration : IEntityTypeConfiguration<GuaranteeEntity>
{
    void IEntityTypeConfiguration<GuaranteeEntity>.Configure(EntityTypeBuilder<GuaranteeEntity> builder)
    {

        builder.ToTable("guarantees");

        builder.HasKey(x => x.Id);

        builder.Property(g => g.OrderId)
            .IsRequired();

        builder.Property(g => g.DateStart)
            .IsRequired();

        builder.Property(g => g.DateEnd)
            .IsRequired();

        builder.Property(g => g.Description)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired(false);

        builder.Property(g => g.Terms)
            .HasMaxLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .IsRequired();

        builder.HasOne(g => g.Order)
            .WithMany(o => o.Guarantees)
            .HasForeignKey(g => g.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
