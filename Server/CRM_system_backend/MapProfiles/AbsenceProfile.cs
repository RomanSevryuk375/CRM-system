using AutoMapper;
using CRMSystem.Core.DTOs.Absence;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class AbsenceProfile : Profile
{
    public AbsenceProfile()
    {
        CreateMap<AbsenceItem, AbsenceResponse>();

        CreateMap<AbsenceUpdateRequest, AbsenceUpdateModel>();

        CreateMap<AbsenceEntity, AbsenceItem>()
            .ForMember(dest => dest.WorkerName,
                        opt => opt.MapFrom(src => $"{src.Worker!.Name} {src.Worker.Surname}"))
            .ForMember(dest => dest.TypeName,
                        opt => opt.MapFrom(src => $"{src.AbsenceType!.Name}"));
    }
}
