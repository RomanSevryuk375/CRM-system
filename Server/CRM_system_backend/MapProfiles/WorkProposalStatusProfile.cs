using AutoMapper;
using CRMSystem.Core.ProjectionModels.WorkProposalStatus;
using CRMSystem.DataAccess.Entities;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class WorkProposalStatusProfile : Profile
{
    public WorkProposalStatusProfile()
    {
        CreateMap<WorkProposalStatusItem, WorkProposalStatusResponse>();

        CreateMap<WorkProposalStatusEntity, WorkProposalStatusItem>();
    }
}
