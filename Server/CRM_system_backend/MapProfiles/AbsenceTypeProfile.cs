using AutoMapper;
using CRM_system_backend.Contracts.AbsenceType;
using CRMSystem.Core.ProjectionModels.AbsenceType;
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
