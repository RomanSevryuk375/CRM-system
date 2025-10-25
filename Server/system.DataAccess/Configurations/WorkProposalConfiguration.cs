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

        builder.Property(wp => wp.Id)
            .HasColumnName("proposal_id")
            .IsRequired();

        builder.Property(wp => wp.OrderId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Property(wp => wp.WorkId)
            .HasColumnName("proposal_work_id")
            .IsRequired();

        builder.Property(wp => wp.ByWorker)
            .HasColumnName("proposed_by_worker_id")
            .IsRequired();

        builder.Property(wp => wp.StatusId)
            .HasColumnName("proposal_status")
            .IsRequired();

        builder.Property(wp => wp.DecisionStatusId)
            .HasColumnName("client_decision")
            .IsRequired();

        builder.Property(wp => wp.Date)
            .HasColumnName("proposed_at")
            .IsRequired();

        builder.HasOne(o => o.Order)
            .WithMany(wp => wp.WorkProposals)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.WorkType)
            .WithMany(wp => wp.WorkProposals)
            .HasForeignKey(wp => wp.WorkId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(w => w.Worker)
            .WithMany(wp => wp.WorkProposals)
            .HasForeignKey(wp => wp.ByWorker)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Status)
            .WithMany(wp => wp.WorkProposals)
            .HasForeignKey(wp => wp.DecisionStatusId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
