using AutoMapper;
using CRMSystem.Core.DTOs.AbsenceType;
using CRMSystem.DataAccess.Entites;

namespace CRM_system_backend.MapProfiles;

public class AbsenceTypeProfile : Profile
{
    public AbsenceTypeProfile()
    {
        CreateMap<AbsenceTypeEntity, AbsenceTypeItem>();

        CreateMap<AbsenceTypeItem, AbsenceTypeResponse>();
    }
}
