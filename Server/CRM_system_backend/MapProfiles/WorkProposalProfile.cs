using AutoMapper;
using CRM_system_backend.Contracts.WorkPropossal;
using CRMSystem.Core.ProjectionModels.WorkProposal;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class WorkProposalProfile : Profile
{
    public WorkProposalProfile()
    {
        CreateMap<WorkProposalItem, WorkProposalResponse>();

        CreateMap<WorkProposalEntity, WorkProposalItem>()
            .ForMember(dest => dest.Job,
                        opt => opt.MapFrom(src => $"{src.Work!.Title}"))
            .ForMember(dest => dest.Worker,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"))
            .ForMember(dest => dest.Status,
                        opt => opt.MapFrom(src => $"{src.Status!.Name}"));
    }
}
