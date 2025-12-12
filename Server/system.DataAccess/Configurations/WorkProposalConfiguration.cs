using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class WorkProposalConfiguration : IEntityTypeConfiguration<WorkProposalEntity>
{
    void IEntityTypeConfiguration<WorkProposalEntity>.Configure(EntityTypeBuilder<WorkProposalEntity> builder)
    {
        builder.ToTable("additional_work_proposals");

        builder.HasKey(x => x.Id);

        builder.Property(wp => wp.OrderId)
            .IsRequired();

        builder.Property(wp => wp.JobId)
            .IsRequired();

        builder.Property(wp => wp.WorkerId)
            .IsRequired();

        builder.Property(wp => wp.StatusId)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(wp => wp.Date)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
            .IsRequired();

        builder.HasOne(o => o.Order)
            .WithMany(wp => wp.WorkProposals)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.Work)
            .WithMany(wp => wp.WorkProposals)
            .HasForeignKey(wp => wp.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.Worker)
            .WithMany(wp => wp.WorkProposals)
            .HasForeignKey(wp => wp.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Status)
            .WithMany(wp => wp.Proposals)
            .HasForeignKey(wp => wp.StatusId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
