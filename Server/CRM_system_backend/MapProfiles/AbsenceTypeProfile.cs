using AutoMapper;
using CRMSystem.Core.ProjectionModels.AbsenceType;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts.AbsenceType;

namespace CRM_system_backend.MapProfiles;

public class AbsenceTypeProfile : Profile
{
    public AbsenceTypeProfile()
    {
        CreateMap<AbsenceTypeEntity, AbsenceTypeItem>();

        CreateMap<AbsenceTypeItem, AbsenceTypeResponse>();
    }
}
