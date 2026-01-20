using AutoMapper;
using CRMSystem.Core.ProjectionModels.Attachment;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.Attachment;

namespace CRM_system_backend.MapProfiles;

public class AttachmentProfile : Profile
{
    public AttachmentProfile()
    {
        CreateMap<AttachmentItem, AttachmentResponse>();

        CreateMap<AttachmentEntity, AttachmentItem>()
            .ForMember(dest => dest.Worker,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"));
    }
}
