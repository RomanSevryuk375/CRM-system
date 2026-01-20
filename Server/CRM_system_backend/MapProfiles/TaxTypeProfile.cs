using AutoMapper;
using CRMSystem.Core.ProjectionModels;
using CRMSystem.DataAccess.Entites;
using Shared.Contracts;

namespace CRM_system_backend.MapProfiles;

public class TaxTypeProfile : Profile
{
    public TaxTypeProfile()
    {
        CreateMap<TaxTypeItem, TaxTypeResponse>();

        CreateMap<TaxTypeEntity, TaxTypeItem>();
    }
}
