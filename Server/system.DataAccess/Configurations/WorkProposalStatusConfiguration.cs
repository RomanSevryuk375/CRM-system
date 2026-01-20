using CRMSystem.Core.Constants;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.Enums;

namespace CRMSystem.DataAccess.Configurations;

public class WorkProposalStatusConfiguration : IEntityTypeConfiguration<WorkProposalStatusEntity>
{
    void IEntityTypeConfiguration<WorkProposalStatusEntity>.Configure(EntityTypeBuilder<WorkProposalStatusEntity> builder)
    {

        builder.ToTable("work_proposal_statuses");

        builder.HasKey(x => x.Id);

        builder.Property(wt => wt.Name)
            .HasMaxLength(ValidationConstants.MAX_STATUS_NAME)
            .IsRequired();

        builder.HasData(
            new WorkProposalStatusEntity
            {
                Id = (int)ProposalStatusEnum.Pending,
                Name = "В ожидании",
            },
            new WorkProposalStatusEntity
            {
                Id = (int)ProposalStatusEnum.Accepted,
                Name = "Принято",
            },
            new WorkProposalStatusEntity
            {
                Id = (int)ProposalStatusEnum.Rejected,
                Name = "Отклонено",
            });

    }
}
