using AutoMapper;
using CRM_system_backend.Contracts;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class WorkProposalStatusProfile : Profile
{
    public WorkProposalStatusProfile()
    {
        CreateMap<WorkProposalStatusItem, WorkProposalStatusResponse>();

        CreateMap<WorkProposalStatusEntity, WorkProposalStatusItem>();
    }
}
