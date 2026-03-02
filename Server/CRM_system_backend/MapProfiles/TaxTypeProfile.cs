using AutoMapper;
using CRMSystem.Core.ProjectionModels.TaxType;
using CRMSystem.DataAccess.Entities;
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
