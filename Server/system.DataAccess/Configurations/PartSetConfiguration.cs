using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class PartSetConfiguration : IEntityTypeConfiguration<PartSetEntity>
{
    void IEntityTypeConfiguration<PartSetEntity>.Configure(EntityTypeBuilder<PartSetEntity> builder)
    {

        builder.ToTable("part_sets");

        builder.HasKey(x => x.Id);

        builder.Property(ps => ps.OrderId)
            .IsRequired(false);

        builder.Property(ps => ps.ProposalId)
            .IsRequired(false);

        builder.Property(ps => ps.PositionId)
            .IsRequired();

        builder.Property(ps => ps.Quantity)
            .HasColumnType("decimal(8, 2)")
            .IsRequired();

        builder.Property(ps => ps.SoldPrice)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne(ps => ps.Order)
            .WithMany(o => o.PartSets)
            .HasForeignKey(ps => ps.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ps => ps.WorkProposal)
            .WithOne(w => w.PartSet)
            .HasForeignKey<PartSetEntity>(ps => ps.ProposalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ps => ps.Position)
            .WithMany(p => p.PartSets)
            .HasForeignKey(ps => ps.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
