using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class ProposedPartConfiguration : IEntityTypeConfiguration<ProposedPartEntity>
{
    void IEntityTypeConfiguration<ProposedPartEntity>.Configure(EntityTypeBuilder<ProposedPartEntity> builder)
    {
        builder.ToTable("proposed_parts");

        builder.HasKey(x => x.Id);

        builder.Property(pp => pp.Id)
            .HasColumnName("proposed_part_id")
            .IsRequired();

        builder.Property(pp => pp.ProposalId)
            .HasColumnName("proposed_parts_Propossal_id")
            .IsRequired();

        builder.Property(pp => pp.UsedPartId)
            .HasColumnName("proposed_parts_")
            .IsRequired();

        builder.HasOne(wp => wp.WorkProposal)
            .WithMany(pp => pp.ProposedParts)
            .HasForeignKey(pp => pp.ProposalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.UsedPart)
            .WithMany(pp => pp.ProposedParts)
            .HasForeignKey(pp => pp.UsedPartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
